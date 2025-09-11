
using System;
using System.Collections.Generic;
using UnityEngine;

public class InGameHolder : SingletonBase<InGameHolder>
{
    protected override bool dontDestroyOnLoad { get; set; } = false;
    public Camera mainCamera;
    
    public List<CharacterBase> Characters { get; } = new List<CharacterBase>(100);
    public List<CharacterBase> Allys { get; } = new List<CharacterBase>(20);
    public List<CharacterBase> Enemies { get; } = new List<CharacterBase>(20);

    public List<CharacterBase> GetCharacters()
    {
        return new List<CharacterBase>(Characters);
    }
    
    public void AddCharacters(CharacterBase character)
    {
        Characters.Add(character);
        if (character.Interface.Collidable.Camp == EColliderCamp.AllyCamp)
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
        if (Characters.Count == 0)
        {
            InGameEventHandler.Instance.CheakGameEndHandler?.Invoke(EColliderCamp.None);
            return;
        }
        
        if (character.Interface.Collidable.Camp == EColliderCamp.AllyCamp)
        {
            Allys.Remove(character);
            if (Allys.Count == 0)
            {
                InGameEventHandler.Instance.CheakGameEndHandler?.Invoke(EColliderCamp.AllyCamp);
            }
        }
        else
        {
            Enemies.Remove(character);
            if (Enemies.Count == 0)
            {
                InGameEventHandler.Instance.CheakGameEndHandler?.Invoke(EColliderCamp.EnemyCamp);
            }
        }
    }
}