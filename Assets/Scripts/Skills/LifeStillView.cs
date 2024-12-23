﻿using Assets.Scripts.Skills.SkillState;
using System;
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
        [SerializeField] private int _range;

        private LineRenderer _lineRenderer;

        public StateMachineSkill StateMachineSkill { get; private set; }

        public ReadyState ReadyState { get; private set; }

        public UsingState UsingState { get; private set; }

        public CooldownState CooldownState { get; private set; }

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
            lifeStillModel.SetRange(_range);

            StateMachineSkill = new StateMachineSkill(lifeStillModel);
            ReadyState = new ReadyState(lifeStillModel);
            UsingState = new UsingState(lifeStillModel);
            CooldownState = new CooldownState(lifeStillModel);

            StateMachineSkill.Initialize(ReadyState);
        }

        private void Update()
        {
            StateMachineSkill.CurrentState.Update();
        }

        private void OnDrawGizmos()
        {
            StateMachineSkill.CurrentState.DrawGizmos();
        }

        public void SelectState(SkillStateType stateType)
        {
            switch (stateType)
            {
                case SkillStateType.Ready:
                    StateMachineSkill.ChangeState(ReadyState);
                    break;

                case SkillStateType.Using:
                    StateMachineSkill.ChangeState(UsingState);
                    break;

                case SkillStateType.Cooldown:
                    StateMachineSkill.ChangeState(CooldownState);
                    break;

                default:
                    Console.WriteLine("Такого состояния нет");
                    break;
            }
        }
    }
}
