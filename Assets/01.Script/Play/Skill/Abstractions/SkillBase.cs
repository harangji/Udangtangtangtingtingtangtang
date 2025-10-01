using UnityEngine;

namespace Udangtangtang.Skill.Abstractions
{
    public abstract class SkillBase
    {
        public SkillData SkillData { get; }
        protected readonly SkillHandler Owner;
        
        private float _currentCoolTime;
        public bool IsOnCooldown => _currentCoolTime > 0;

        protected SkillBase(SkillData skillData, SkillHandler owner)
        {
            SkillData = skillData;
            Owner = owner;
        }

        /// <summary>
        /// 스킬을 발동시킵니다.
        /// </summary>
        public void Activate()
        {
            if (IsOnCooldown)
            {
                Debug.Log($"{SkillData.skillName} is on cooldown.");
                return;
            }

            OnActivate();
            StartCooldown();
        }

        /// <summary>
        /// 매 프레임 호출되어 쿨타임을 감소시킵니다.
        /// </summary>
        public void Tick()
        {
            if (IsOnCooldown)
            {
                _currentCoolTime -= Time.deltaTime;
                if (_currentCoolTime <= 0)
                {
                    _currentCoolTime = 0;
                    OnCooldownEnd();
                }
            }
        }

        /// <summary>
        /// 스킬의 실제 로직이 구현되는 부분입니다. (자식 클래스에서 구현)
        /// </summary>
        protected abstract void OnActivate();

        /// <summary>
        /// 쿨타임이 시작될 때 호출됩니다.
        /// </summary>
        private void StartCooldown()
        {
            _currentCoolTime = SkillData.coolTime;
        }

        /// <summary>
        /// 쿨타임이 종료될 때 호출됩니다. (자식 클래스에서 오버라이드 가능)
        /// </summary>
        protected virtual void OnCooldownEnd()
        {
            Debug.Log($"{SkillData.skillName} is ready.");
        }
    }
}
