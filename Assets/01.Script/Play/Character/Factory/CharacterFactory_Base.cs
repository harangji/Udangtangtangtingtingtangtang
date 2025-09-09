
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterFactory_Base : MonoBehaviour
{
    public GameObject[] mCharacterPrefabs;
    
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
        // else if (touch.phase == TouchPhase.Began)
        // {

        // }
        // else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) 
        // {

        // }
    }

    public void BakeCharacter(Vector2 vector2)
    {
        GameObject copy = Instantiate(mCharacterPrefabs[Random.Range(0,2)], vector2, Quaternion.identity, transform);
        if (copy.TryGetComponent(out CharacterBase bakedCharacter))
        {
            // bakedCharacter.spriteRenderer.sprite = null;
            bakedCharacter.Injection();
        }
    }
}