using UnityEngine;

namespace Assets.Scripts.Service.Dead
{
    public class Dead: MonoBehaviour, IDead
    {
        public bool IsDead {  get; private set; }

        private void Start()
        {
            IsDead = false;
        }

        public void SetDead()
        {
            IsDead = true;
            Destroy(gameObject);
        }
    }
}
