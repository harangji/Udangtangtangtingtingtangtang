using System.Collections.Generic;
using UnityEngine;

public class CharacterSummonUI : MonoBehaviour
{
    [Header("UI 설정")]
    [Tooltip("드래그 가능한 아이콘들을 담을 부모 Transform (예: ScrollView의 Content)")]
    [SerializeField] private Transform _iconContainer;

    [Tooltip("아이콘으로 사용할 UI 프리팹. DraggableCharacterIcon.cs 스크립트가 있어야 합니다.")]
    [SerializeField] private GameObject _iconPrefab;

    [Header("소환할 캐릭터 목록")]
    [SerializeField] private List<SummonableCharacterData> _summonableCharacters;

    void Start()
    {
        if (_iconContainer == null || _iconPrefab == null)
        {
            Debug.LogError("CharacterSummonUI: UI 설정이 제대로 되지 않았습니다. Container와 Prefab을 확인해주세요.");
            return;
        }

        PopulateUI();
    }

    private void PopulateUI()
    {
        // 기존 아이콘들 삭제
        foreach (Transform child in _iconContainer)
        {
            Destroy(child.gameObject);
        }

        // 리스트에 있는 캐릭터들로 UI 채우기
        foreach (var charData in _summonableCharacters)
        {
            if (charData == null) continue;

            GameObject iconGO = Instantiate(_iconPrefab, _iconContainer);
            DraggableCharacterIcon draggableIcon = iconGO.GetComponent<DraggableCharacterIcon>();

            if (draggableIcon != null)
            {
                draggableIcon.Initialize(charData);
            }
            else
            {
                Debug.LogError($"Icon Prefab에 DraggableCharacterIcon.cs 스크립트가 없습니다!");
            }
        }
    }
}
