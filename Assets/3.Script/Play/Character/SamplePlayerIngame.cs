using System.Threading.Tasks;
using UnityEngine;

public class SamplePlayerIngame : CharacterBase
{
    [field: SerializeField] public ECharacterType Type { get; set; }
    [field: SerializeField] public EColliderCamp Camp { get; set; }
    [field: SerializeField] public CircleCollider2D Col { get; private set;}
    [field: SerializeField] public Rigidbody2D Rb { get; private set; }

    protected override void Injection()
    {
        Interface.Character = new CharacterInfo(Type, new CharacterStat());
        Collidable collidable = gameObject.AddComponent<Collidable>();
        collidable.Init(this, Camp, Col, Rb);
        Interface.Collidable = collidable;

        Interface.Damageable = new Damageable(this);
        Interface.Healable = new Healable(this);
    }

    public override Task ExecuteSkill(int index, CharacterBase[] target = null)
    {
        Task.Delay(1, destroyCancellationToken);
        return Task.CompletedTask;
    }
}
