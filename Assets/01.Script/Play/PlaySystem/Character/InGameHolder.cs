
using System;
using System.Collections.Generic;
using UnityEngine;

public class InGameHolder : SingletonBase<InGameHolder>
{
    protected override bool dontDestroyOnLoad { get; set; } = false;
    public Camera mainCamera;
    public EnemySpawner enemySpawner; // 적 스포너 참조
    public CharacterBase playerCharacter; // 플레이어 캐릭터 참조

    public List<CharacterBase> Characters { get; } = new List<CharacterBase>(100);
    public List<CharacterBase> Allys { get; } = new List<CharacterBase>(20);
    public List<CharacterBase> Enemies { get; } = new List<CharacterBase>(20);

    private void Start()
    {
        // playerCharacter가 인스펙터에서 할당되었는지 확인합니다.
        if (enemySpawner != null && playerCharacter != null)
        {
            // 할당된 플레이어를 타겟으로 스폰을 시작합니다.
            enemySpawner.BeginSpawning(playerCharacter.transform);
        }
        else if (enemySpawner != null)
        {
            // 플레이어가 할당되지 않았을 경우 경고를 표시합니다.
            Debug.LogWarning("InGameHolder: 플레이어 캐릭터가 할당되지 않았습니다. 스포너가 시작되지 않습니다.");
        }
    }

    public List<CharacterBase> GetCharacters()
    {
        return new List<CharacterBase>(Characters);
    }
    
    public void AddCharacters(CharacterBase character)
    {
        Characters.Add(character);
        if (character.Camp == EColliderCamp.AllyCamp)
        {
            Allys.Add(character);
        }
        else
        {
            Enemies.Add(character);
        }
    }
    
    public void RemoveCharacters(CharacterBase character)
    {
        Characters.Remove(character);
    }
}