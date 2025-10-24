using System.Linq;
using UnityEngine;
using System.Threading.Tasks;

public abstract class SkillBase
    {
        public SkillData SkillData { get; }
        protected readonly BaseSkillHandler Owner;
        
        private float _currentCoolTime;
        public bool IsOnCooldown => _currentCoolTime > 0;

        protected SkillBase(SkillData skillData, BaseSkillHandler owner)
        {
            SkillData = skillData;
            Owner = owner;
        }

        /// <summary>
        /// 스킬을 발동시킵니다.
        /// </summary>
        public async void Activate()
        {
            if (IsOnCooldown)
            {
                Debug.Log($"{SkillData.skillName} is on cooldown.");
                return;
            }

            await OnActivateAsync();
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
        protected abstract Task OnActivateAsync();

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

        /// <summary>
        /// 게임 내 플레이어 캐릭터를 찾아 반환합니다.
        /// </summary>
        protected CharacterBase FindPlayer()
        {
            // 플레이어 캐릭터는 AllyCamp에 속한다고 가정합니다.
            // TODO: 만약 플레이어 캐릭터를 식별하는 더 정확한 방법(예: 태그, 특정 컴포넌트)이 있다면 수정해야 합니다.
            return InGameHolder.Instance.Characters.FirstOrDefault(c => c.Camp == EColliderCamp.AllyCamp && c.BAlive);
        }
    }
