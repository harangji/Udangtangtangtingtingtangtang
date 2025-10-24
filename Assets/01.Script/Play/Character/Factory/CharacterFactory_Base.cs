
using UnityEngine;

public class CharacterFactory_Base : MonoBehaviour
{
    public GameObject[] mCharacterPrefabs;
    [SerializeField] private GameObject parent;
    private TouchInputProvider touchInputProvider;

    private void OnEnable()
    {
        touchInputProvider = TouchInputProvider.Instance;
        if (touchInputProvider != null)
        {
            touchInputProvider.OnTouchEnded += BakeCharacter;
        }
        else
        {
            Debug.LogError("CharacterFactory_Base: TouchInputProvider.Instance를 찾을 수 없습니다. 터치 입력 생성을 비활성화합니다.");
            this.enabled = false;
        }
    }

    private void OnDisable()
    {
        touchInputProvider.OnTouchEnded -= BakeCharacter;
    }

    public void BakeCharacter(Vector2 vector2)
    {
        if (mCharacterPrefabs == null || mCharacterPrefabs.Length == 0)
        {
            Debug.LogError("CharacterFactory_Base: 소환할 캐릭터 프리팹이 없습니다.");
            return;
        }

        if (parent == null)
        {
            Debug.LogError("CharacterFactory_Base: 생성된 캐릭터가 할당될 부모 오브젝트가 지정되지 않았습니다.");
            return;
        }

        Instantiate(mCharacterPrefabs[Random.Range(0, mCharacterPrefabs.Length)], vector2, Quaternion.identity, parent.transform);
    }
}