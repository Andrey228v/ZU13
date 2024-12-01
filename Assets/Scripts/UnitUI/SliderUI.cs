using UnityEngine.UI;

using UnityEngine;
using Assets.Scripts.Service.Health;

namespace Assets.Scripts.UnitUI
{
    [RequireComponent(typeof(HealthUnits))]
    public class SliderUI: MonoBehaviour
    {
        [field:SerializeField] public Slider Slider { get; private set; }

        public HealthUnits Health {get; private set;}

        public virtual void Awake()
        {
            Health = GetComponent<HealthUnits>();

            Health.Damaged += ChangeData;
            Health.Healed += ChangeData;
        }

        private void Start()
        {
            Slider.value = Health.Amount;
            Slider.maxValue = Health.MaxAmount;
        }

        private void OnDestroy()
        {
            Health.Damaged -= ChangeData;
            Health.Healed -= ChangeData;
        }

        private void Update()
        {
            UpdateAmount();
        }

        public virtual void ChangeData(){}

        public virtual void UpdateAmount(){}
    }
}
