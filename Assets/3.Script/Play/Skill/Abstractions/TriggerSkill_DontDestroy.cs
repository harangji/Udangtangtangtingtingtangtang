// using System;
// using System.Threading.Tasks;
// using UnityEngine;
//
// /// <summary>
// /// 맞아도 사라지지 않고 연속 타격되는 스킬
// /// </summary>
// public abstract class TriggerSkill_DontDestroy : SkillBase
// {
//     protected abstract float mTriggerTimer { get; set; }
//     protected abstract float mTriggerInterval { get; set; }
//     
//     protected virtual void Update()
//     {
//         mTriggerTimer += Time.deltaTime;
//     }
//
//     protected virtual void OnTriggerStay2D(Collider2D other)
//     {
//         if(mTriggerTimer < mTriggerInterval) return;
//         if (!other.TryGetComponent(out CharacterBase col)) return;
//         if (col.ColliderCamp != TargetColliderCamp) return;
//         
//         CombatSystem.Instance.AddCombatEvent(
//             new CollideEvent()
//             {
//                 Sender = SkillCaster,
//                 Receiver = col,
//                 Skill = this
//             }
//         );
//     }
// }
