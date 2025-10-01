using UnityEngine;

[CreateAssetMenu(fileName = "New SummonableCharacter", menuName = "Udangtangtang/Summonable Character Data", order = 1)]
public class SummonableCharacterData : ScriptableObject
{
    [Header("소환할 실제 캐릭터 프리팹")]
    public GameObject characterPrefab;

    [Header("UI에 표시될 정보")]
    public string characterName;
    public Sprite uiIcon;
    [TextArea]
    public string description;
}
