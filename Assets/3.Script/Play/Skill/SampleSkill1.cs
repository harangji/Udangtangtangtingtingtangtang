
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SampleSkill1 : TriggerSkill, ISkillTypeAttack<IInGameCharacter[]>
{
    public override EColliderCamp TargetColliderCamp { get; set; } = EColliderCamp.AllyCamp;
    public override IInGameCharacter SkillCaster { get; set; }
    public override TaskCompletionSource<bool> Tcs { get; set; }
    
    public float AttackAmount { get; set; }
    public float BuffAmount { get; set; }
    
    private float mSkillDamage = 0f;
    
    public override async Task ActivateSkill(IInGameCharacter sender, CombatEvent[] target)
    {
        SkillCaster = sender;
        mSkillDamage = SkillCaster.UnitStat.Attack * AttackAmount;
        // await Tcs.Task;
        return;
    }
    
    public override void OnCollide(CombatEvent combatEvent)
    {
        
    }
    
    public override void ApplySkill(IInGameCharacter[] target)
    {

    }
    
    public void ApplyAttack(IInGameCharacter[] target)
    {

    }
}

