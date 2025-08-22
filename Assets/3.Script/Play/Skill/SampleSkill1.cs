
using System.Collections.Generic;
using System.Threading.Tasks;

public class SampleSkill1 : SkillBase, ISkillTypeAttack<CharacterBase[]>, ISkillTypeDebuff<CharacterBase>
{
    public float AttackAmount { get; private set; } = 0.5f;
    public override TaskCompletionSource<bool> Tcs { get; set; } = new TaskCompletionSource<bool>();
    
    public void ExecuteAttack(CharacterBase[] target)
    {
        AttackAmount = 10;
    }
    
    public float BuffAmount { get; }
    
    public Task ExecuteDeBuff(CharacterBase target)
    {
        return Tcs.Task;
    }
}

