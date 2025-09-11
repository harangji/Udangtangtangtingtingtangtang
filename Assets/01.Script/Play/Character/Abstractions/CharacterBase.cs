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
    public bool BAlive { get; set; } = true;
    protected bool BCompleteInjection { get; set; } = false;
    public InjectionInterface Interface = new InjectionInterface();
    public Animator animator;
    
    protected virtual void Awake()
    {
        if(!BCompleteInjection)
            Injection();

        if (Interface.Collidable != null)
        {
            gameObject.AddComponent<OnCollisionClass>().character = this;
            InGameHolder.Instance.Characters.Add(this);
        }
    }

    public abstract void Injection();
    
    public abstract Task ExecuteSkill(int index, CharacterBase[] target = null);
}