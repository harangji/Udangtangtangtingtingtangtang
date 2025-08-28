
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SampleSkill1 : SkillBase, ISkillTypeAttack<CharacterBase[]>, ISkillTypeDebuff<CharacterBase>
{
    public override EColliderCamp ColliderCamp { get; set; }
    public override ICharacter Sender { get; set; }
    public override TaskCompletionSource<bool> Tcs { get; set; }
    public override void ExecuteSkill(ICharacter sender, ICombatEvent[] target)
    {
        Sender = sender;
    }

    public override void ApplySkill(ICharacter[] target)
    {
        throw new NotImplementedException();
    }

    public override void OnCollide(ICharacter target)
    {
        throw new NotImplementedException();
    }

    public float AttackAmount { get; }
    public void ExecuteAttack(CharacterBase[] target)
    {
        throw new NotImplementedException();
    }

    public float BuffAmount { get; }
    public Task ExecuteDeBuff(CharacterBase target)
    {
        throw new NotImplementedException();
    }
}

