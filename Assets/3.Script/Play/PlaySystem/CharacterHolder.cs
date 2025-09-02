
using System.Collections.Generic;
using UnityEngine;

public class CharacterHolder : SingletonBase<CharacterHolder>
{
    protected override bool dontDestroyOnLoad { get; set; } = false;
    
    public List<CharacterBase> Allys { get; set; }= new List<CharacterBase>();
    public List<CharacterBase> Enemies { get; set; } = new List<CharacterBase>();
}