
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SampleSkill1 : SkillBase, ISkillTypeAttack<CharacterBase[]>
{
    public override EColliderCamp TargetColliderCamp { get; set; } = EColliderCamp.AllyCamp;
    public override CharacterBase SkillCaster { get; set; }
    public override TaskCompletionSource<bool> Tcs { get; set; }
    
    public float AttackAmount { get; set; }


    public float BuffAmount { get; set; }
    
    private float mSkillDamage = 0f;
    
    public override async Task ActivateSkill(CharacterBase sender, CollideEvent[] target)
    {
        SkillCaster = sender;
        mSkillDamage = SkillCaster.UnitStat.Attack * AttackAmount;
        // await Tcs.Task;
        return;
    }
    
    public void ApplyAttack(CharacterBase[] target)
    {

    }
}

