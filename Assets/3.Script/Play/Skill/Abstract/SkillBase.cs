using System.Threading.Tasks;
using UnityEngine;


public abstract class SkillBase : MonoBehaviour, ISkill
{
    public abstract TaskCompletionSource<bool> Tcs { get; set; }
}
