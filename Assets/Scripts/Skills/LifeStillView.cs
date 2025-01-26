using UnityEngine;

namespace Assets.Scripts.Skills
{
    [RequireComponent(typeof(LineRenderer))]
    public class LifeStillView : MonoBehaviour
    {
        private ISkillUser _user;
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void SetLinePosition(Vector3 startPoint, Vector3 endPoint)
        {
            _lineRenderer.SetPosition(0, startPoint);
            _lineRenderer.SetPosition(1, endPoint);
        }

        public void DeletLine()
        {
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, Vector3.zero);
        }
    }
}
