using Assets.Scripts.Skills.SkillState;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class LifeStillView : MonoBehaviour, ISkillView
    {
        [SerializeField] private Player _player;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private TMP_Text _ui;
        [SerializeField] private int _useTime;
        [SerializeField] private int _cooldown;
        [SerializeField] private int _damage;
        [SerializeField] private int _heal;
        [SerializeField] private int _range;

        private LineRenderer _lineRenderer;

        public StateMachineSkill StateMachineSkill { get; private set; }
        
        private void Awake()
        {
            _lineRenderer = _player.AddComponent<LineRenderer>();
        }

        private void Start()
        {
            LifeStillModel lifeStillModel = new LifeStillModel();
            lifeStillModel.SetPlayer(_player);
            lifeStillModel.SetLineRenderer(_lineRenderer);
            lifeStillModel.SetUI(_ui);
            lifeStillModel.SetTargetLayer(_targetLayer.value);
            lifeStillModel.SetUseTime(_useTime);
            lifeStillModel.SetCooldown(_cooldown);
            lifeStillModel.SetDamage(_damage);
            lifeStillModel.SetHeal(_heal);
            lifeStillModel.SetRange(_range);

            StateMachineSkill = new StateMachineSkill(lifeStillModel);
            SkillStateType startState = SkillStateType.Ready;
            StateMachineSkill.Initialize(startState);
        }

        private void Update()
        {
            StateMachineSkill.CurrentState.Update();
        }

        private void OnDrawGizmos()
        {
            StateMachineSkill.CurrentState.DrawGizmos();
        }
    }
}
