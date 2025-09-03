
using System.Threading.Tasks;
using Sirenix.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class SamplePlayerIngame : CharacterBase, IHealable
{
    public override ISkill[] Skills { get; set; }
    
    public void TakeHeal(int amount)
    {
        UnitStat.Hp += amount;
        CharacterCallBack.ShowDamageAction?.Invoke(amount, transform.position, Color.green);
    }
    
    public override Task ExecuteSkill(int index, CharacterBase[] target = null)
    {
        Task.Delay(1, destroyCancellationToken);
        return Task.CompletedTask;
    }
}
