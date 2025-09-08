
using UnityEngine;

public class CharacterFactory_Base : MonoBehaviour
{
    public GameObject CharacterPrefab;
    public CharacterBase test;

    private void Start()
    {
        test = CharacterPrefab.GetComponent<CharacterBase>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BakeCharacter(test.Interface);
        }
    }

    public void BakeCharacter(InjectionInterface injectionInterface)
    {
        if (Instantiate(CharacterPrefab, new Vector2(0, 0), Quaternion.identity, transform).TryGetComponent(out CharacterBase bakedCharacter))
        {
            bakedCharacter.Interface = injectionInterface;
        }
    }
}