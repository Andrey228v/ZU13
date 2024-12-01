using TMPro;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public class LifeStillModel
    {
        public Player Player {get; private set;}

        public LineRenderer LineRenderer { get; private set; }

        public TMP_Text UI {get; private set;}

        public EnemyBody Target {get; private set;}

        public int TargetLayer { get; private set; }

        public int UseTime { get; private set; }

        public int Cooldown { get; private set; }

        public int Damage { get; private set; }

        public int Heal { get; private set; }

        public int Range {  get; private set; }

        public void SetPlayer(Player player) => Player = player;
        public void SetLineRenderer(LineRenderer lineRenderer) => LineRenderer = lineRenderer; 
        public void SetUI(TMP_Text ui) => UI = ui; 
        public void SetTarget(EnemyBody target) => Target = target; 
        public void SetTargetLayer(int targetLayer) => TargetLayer = targetLayer; 
        public void SetUseTime(int useTime) => UseTime = useTime; 
        public void SetCooldown(int cooldown) => Cooldown = cooldown; 
        public void SetDamage(int damage) => Damage = damage; 
        public void SetHeal(int heal) => Heal = heal; 
        public void SetRange(int range) => Range = range; 
    }
}
