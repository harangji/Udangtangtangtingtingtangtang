
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterFactory_Base : MonoBehaviour
{
    public GameObject[] mCharacterPrefabs;
    [SerializeField] private GameObject parent;
    
    public void Update()
    {
        if (Input.touchCount == 0) return;
        
        Touch touch = Input.GetTouch(0);
        
        if (touch.phase is TouchPhase.Ended or TouchPhase.Canceled)
        {
            Vector3 worldPos = InGameHolder.Instance.mainCamera.ScreenToWorldPoint(touch.position);
            worldPos.z = 0f; // 2D라면 Z축을 0으로 설정
            
            BakeCharacter(worldPos);
        }
    }

    public void BakeCharacter(Vector2 vector2)
    {
        Instantiate(mCharacterPrefabs[Random.Range(0, mCharacterPrefabs.Length)], vector2, Quaternion.identity, parent.transform);
    }
}