using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class InjectionInterface
{
    public ICharacter Character { get; set; }
    public ICollidable Collidable { get; set; }
    public IDamageable Damageable { get; set; }
    public IHealable Healable { get; set; }
}

public abstract class CharacterBase : MonoBehaviour
{
    public bool bAlive { get; set; } = true;
    public InjectionInterface Interface = new InjectionInterface();
    
    protected virtual void Awake()
    {
        Injection();
    }

    protected abstract void Injection();
    
    public abstract Task ExecuteSkill(int index, CharacterBase[] target = null);
}