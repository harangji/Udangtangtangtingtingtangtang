
using System;
using System.Collections.Generic;
using UnityEngine;

public class InGameHolder : SingletonBase<InGameHolder>
{
    protected override bool dontDestroyOnLoad { get; set; } = false;
    public Camera mainCamera;
    public List<CharacterBase> Allys { get; set; }= new List<CharacterBase>(20);
    public List<CharacterBase> Enemies { get; set; } = new List<CharacterBase>(20);
}