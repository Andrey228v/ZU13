using UnityEngine;

namespace Assets.Scripts.Skills
{
    [RequireComponent(typeof(LineRenderer))]
    public class LifeStillView : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private int _backGorundCoordZ = -1;

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

        public void DrawLineFromUserToTarget(Transform user, Transform target)
        {
            SetLinePosition(
                        new Vector3(user.position.x, user.position.y, _backGorundCoordZ),
                        new Vector3(target.position.x, target.position.y, _backGorundCoordZ));
        }
    }
}
