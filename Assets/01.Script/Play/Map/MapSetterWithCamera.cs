using System;
using System.Collections.Generic;
using UnityEngine;

public class MapSetterWithCamera : MonoBehaviour
{
    public EdgeCollider2D edge;
    private Camera targetCamera;

    private float height;
    private float worldLeft;
    private float worldRight;
    private float worldBottom;
    private float worldTop;
    private Vector3 targetPosition;
    
    private void Awake()
    {
        targetCamera = InGameHolder.Instance.mainCamera;
        height = targetCamera.orthographicSize;
        UpdateCollider();
    }

    private void Start()
    {
        float halfWidth = height * targetCamera.aspect;
        
        Vector3 cameraPosition = targetCamera.transform.position;
        
        worldLeft   = cameraPosition.x - halfWidth;
        worldRight  = cameraPosition.x + halfWidth;
        worldBottom = cameraPosition.y - height;
        worldTop    = cameraPosition.y + height;
    }

    private void FixedUpdate()
    {
        List<CharacterBase> characters = new List<CharacterBase>(InGameHolder.Instance.Characters);
        
        foreach (CharacterBase character in characters)
        {
            targetPosition = character.transform.position;
            if (targetPosition.x < worldLeft || targetPosition.x > worldRight || 
                targetPosition.y < worldBottom || targetPosition.y > worldTop)
            {
                Debug.Log(character.gameObject.name + "이(가) 경계를 벗어나 원점으로 돌아갑니다.");
                character.transform.position = Vector2.zero;
            }
        }
    }

    
    private void UpdateCollider()
    {
        if (targetCamera == null) return;
        
        float height = targetCamera.orthographicSize * 2f;
        float width = height * targetCamera.aspect;

        float left   = -width * 0.5f;
        float right  =  width * 0.5f;
        float bottom = -height * 0.5f;
        float top    =  height * 0.5f;
        
        Vector2[] points = new Vector2[5];
        points[0] = new Vector2(left, bottom);
        points[1] = new Vector2(left, top);
        points[2] = new Vector2(right, top);
        points[3] = new Vector2(right, bottom);
        points[4] = new Vector2(left, bottom);

        edge.points = points;
        transform.position = targetCamera.transform.position;
    }
}