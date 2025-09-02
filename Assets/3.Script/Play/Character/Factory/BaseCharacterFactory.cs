
using System.Collections.Generic;
using UnityEngine;

// public abstract class BaseCharacterFactory : MonoBehaviour
// {
//     public FactoryResourceHolder ResourceHolder;
//     public Dictionary<string, CharacterBase> BaseCharacterDic = new Dictionary<string, CharacterBase>();
//     public abstract CharacterBase BakeCharacter(string key);
// }

public class BaseCharacterFactory
{
    public FactoryResourceHolder ResourceHolder;
    public Dictionary<string, CharacterBase> BaseCharacterDic = new Dictionary<string, CharacterBase>();
    
    public CharacterBase BakeCharacter(string key)
    {
        CharacterContext context = ResourceHolder.characterDataSO.GetData(key);
        CharacterBase target = BaseCharacterDic[key];
        
        return target.InitializeStat(context);
    }
}