
using System;
using UnityEngine;

public class CharacterStatMaker : SingletonBase<CharacterStatMaker>
{
    protected override bool dontDestroyOnLoad { get; set; } = true;

    private void Awake()
    {
        
    }
}