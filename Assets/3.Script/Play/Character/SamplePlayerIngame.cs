
using System.Threading.Tasks;
using Sirenix.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class SamplePlayerIngame : CharacterBase
{
    public override ISkill[] Skills { get; set; }
    
    public override Task ExecuteSkill(int index, CharacterBase[] target = null)
    {
        Task.Delay(1, destroyCancellationToken);
        return Task.CompletedTask;
    }
}
