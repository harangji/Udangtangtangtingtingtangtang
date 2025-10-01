using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class PrefabThumbnailGenerator : EditorWindow
{
    // Fields for settings
    private int _textureSize = 512;
    private float _padding = 1.2f;
    private Vector3 _rotation = Vector3.zero;
    private string _savePath = "Assets/Sprite/CharacterSprite";

    // Fields for the prefab list
    [SerializeField] // Needs to be serialized to be used with SerializedProperty
    private List<GameObject> _prefabs = new List<GameObject>();
    private SerializedObject _serializedObject;
    private SerializedProperty _prefabsProperty;
    private Vector2 _scrollPosition; // For the list scroll view

    [MenuItem("Tools/Udangtangtang/Prefab Thumbnail Generator")]
    public static void ShowWindow()
    {
        GetWindow<PrefabThumbnailGenerator>("Prefab Thumbnail");
    }

    private void OnEnable()
    {
        // Setup SerializedObject and Property for the list
        _serializedObject = new SerializedObject(this);
        _prefabsProperty = _serializedObject.FindProperty("_prefabs");
    }

    private void OnGUI()
    {
        _serializedObject.Update();

        GUILayout.Label("Prefab Thumbnail Generator", EditorStyles.boldLabel);
        
        // Use a scroll view for the main content
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

        // The list of prefabs. Drag and drop is handled automatically.
        EditorGUILayout.PropertyField(_prefabsProperty, new GUIContent("Prefabs to Process"), true);

        EditorGUILayout.Space();

        // Settings fields
        GUILayout.Label("Thumbnail Settings", EditorStyles.boldLabel);
        _textureSize = EditorGUILayout.IntField("Texture Size", _textureSize);
        _padding = EditorGUILayout.FloatField("Padding", _padding);
        _rotation = EditorGUILayout.Vector3Field("Rotation", _rotation);
        
        GUILayout.Label("Save Path", EditorStyles.boldLabel);
        _savePath = EditorGUILayout.TextField("Path", _savePath);

        EditorGUILayout.EndScrollView();

        // Generate button
        if (GUILayout.Button("Generate Thumbnails"))
        {
            if (_prefabs.Count == 0)
            {
                EditorUtility.DisplayDialog("오류", "목록에 프리팹을 하나 이상 추가해주세요.", "OK");
                return;
            }

            GenerateThumbnails(_prefabs);
        }

        _serializedObject.ApplyModifiedProperties();
    }

    private void GenerateThumbnails(List<GameObject> prefabs)
    {
        if (string.IsNullOrEmpty(_savePath))
        {
            EditorUtility.DisplayDialog("오류", "저장 경로를 지정해주세요.", "OK");
            return;
        }

        if (!Directory.Exists(_savePath))
        {
            Directory.CreateDirectory(_savePath);
        }

        int generatedCount = 0;
        foreach (var prefab in prefabs)
        {
            if (prefab == null) continue; // Skip null entries in the list

            if (GenerateThumbnailForPrefab(prefab))
            {
                generatedCount++;
            }
        }
        
        AssetDatabase.Refresh();
        Debug.Log($"{generatedCount}개의 썸네일 생성이 완료되었습니다. 저장 위치: {_savePath}");
        EditorUtility.DisplayDialog("완료", $"{generatedCount}개의 썸네일 생성이 완료되었습니다.", "OK");
    }

    private bool GenerateThumbnailForPrefab(GameObject prefab)
    {
        // --- 임시 씬 설정 ---
        GameObject instance = Instantiate(prefab, Vector3.zero, Quaternion.Euler(_rotation));

        var animators = instance.GetComponentsInChildren<Animator>();
        foreach (var anim in animators)
        {
            anim.speed = 0;
        }

        var renderers = instance.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
        {
            Debug.LogWarning($"프리팹 '{prefab.name}'에서 Renderer를 찾을 수 없어 썸네일을 생성할 수 없습니다.");
            DestroyImmediate(instance);
            return false;
        }

        Bounds bounds = new Bounds(instance.transform.position, Vector3.zero);
        foreach (var rend in renderers)
        {
            bounds.Encapsulate(rend.bounds);
        }

        // --- 카메라 설정 ---
        GameObject cameraGO = new GameObject("ThumbnailCamera");
        Camera camera = cameraGO.AddComponent<Camera>();
        camera.cullingMask = -1;
        camera.orthographic = false;
        camera.fieldOfView = 30f;
        camera.backgroundColor = new Color(0, 0, 0, 0);
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.allowHDR = false;
        camera.allowMSAA = false;

        float objectSize = bounds.size.magnitude;
        if (objectSize < 0.01f) objectSize = 1f; // Avoid division by zero or tiny sizes
        float cameraDistance = (objectSize / 2.0f / Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad)) * _padding;
        camera.transform.position = bounds.center - camera.transform.forward * cameraDistance;

        // --- 렌더링 ---
        RenderTexture rt = new RenderTexture(_textureSize, _textureSize, 24, RenderTextureFormat.ARGB32);
        rt.antiAliasing = 8;
        camera.targetTexture = rt;
        camera.Render();

        // --- 픽셀 읽기 및 저장 ---
        RenderTexture.active = rt;
        Texture2D thumbnail = new Texture2D(_textureSize, _textureSize, TextureFormat.ARGB32, false);
        thumbnail.ReadPixels(new Rect(0, 0, _textureSize, _textureSize), 0, 0);
        thumbnail.Apply();
        RenderTexture.active = null;

        byte[] bytes = thumbnail.EncodeToPNG();

        // --- 파일 저장 (덮어쓰기) ---
        string path = Path.Combine(_savePath, prefab.name + ".png");
        File.WriteAllBytes(path, bytes);
        
        // --- 정리 ---
        DestroyImmediate(instance);
        DestroyImmediate(cameraGO);
        DestroyImmediate(rt);
        DestroyImmediate(thumbnail);

        Debug.Log($"썸네일 저장 완료: {path}");
        return true;
    }
}
