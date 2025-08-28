
using System.Threading.Tasks;
using Sirenix.Serialization;
using UnityEngine;

public class SamplePlayer1 : CharacterBase
{
    [field : SerializeField] public override ECharacterType Type { get; set; }
    [field : SerializeField] public override EColliderCamp ColliderCamp { get; set; }
    [OdinSerialize] public override ISkill[] Skills => new ISkill[3];
    
    public override CharacterBase Character => this;

    public override void InitializeStat(ICharacter.Stat stat)
    {
        UnitStat = stat;
    }
    
    public override void OnCollide(ICharacter target)
    {
        Shove(target.Character);
    }



    public override void Shove(CharacterBase target)
    {
        Vector2 dir = (transform.position - target.transform.position).normalized;
        target.Rb.AddForce( -dir * 1000f); //나와 반대 방향으로 밀기
    }

    public override void Shoved(CharacterBase target)
    {
        
    }

    protected override void TakeDamage(int damage)
    {
        UnitStat.Hp -= damage;
    }

    public override Task ExecuteSkill()
    {
        Task.Delay(1, destroyCancellationToken);
        return Task.CompletedTask;
    }

    private void OnDrawGizmos()
    {
        
    }
}
