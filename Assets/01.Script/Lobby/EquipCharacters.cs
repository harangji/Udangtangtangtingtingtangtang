
using System;

public class EquipCharacters : SingletonBase<EquipCharacters>
{
    protected override bool dontDestroyOnLoad { get; set; } = false;
    
    private void Start()
    {
        
    }
}
