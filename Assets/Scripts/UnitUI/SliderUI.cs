using UnityEngine.UI;
using Assets.Scripts.Service;
using UnityEngine;

namespace Assets.Scripts.UnitUI
{
    [RequireComponent(typeof(IHealth))]
    public class SliderUI: MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public IHealth Health {get; private set;}

        public Slider Slider { get; private set; }

        public virtual void Awake()
        {
            Health = GetComponent<IHealth>();
            Slider = _slider;

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
