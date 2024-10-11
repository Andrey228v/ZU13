using UnityEngine.UI;
using UnityEngine;
using Assets.Scripts.Service.Health;

namespace Assets.Scripts.UnitUI
{
    [RequireComponent(typeof(Button))]
    public class ButtonUI: MonoBehaviour
    {
        [SerializeField] private Health _health;

        public Button Button {get; private set;}
        public Health Health { get; private set; }

        public virtual void Awake()
        {
            Button = GetComponent<Button>();
            Health = _health;
        }
    }
}
