using UnityEngine;

namespace Assets.Scripts.Service
{
    public interface ITarget
    {
        public IDead Dead { get; }

        public void DetectedByEnemy();

        public void UndetectedByEnemy();

        public Vector2 GetPosition();
    }
}
