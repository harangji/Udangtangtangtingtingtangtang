
using UnityEngine;

public class CharacterFactory_Base : MonoBehaviour
{
    public GameObject[] mCharacterPrefabs;
    [SerializeField] private GameObject parent;
    private TouchInputProvider touchInputProvider;

    private void OnEnable()
    {
        touchInputProvider = TouchInputProvider.Instance;
        touchInputProvider.OnTouchEnded += BakeCharacter;
    }

    private void OnDisable()
    {
        touchInputProvider.OnTouchEnded -= BakeCharacter;
    }

    public void BakeCharacter(Vector2 vector2)
    {
        Instantiate(mCharacterPrefabs[Random.Range(0, mCharacterPrefabs.Length)], vector2, Quaternion.identity, parent.transform);
    }
}