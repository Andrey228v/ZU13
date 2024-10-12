using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Player _unit;

    private Camera _worldCamera;

    private void Start()
    {
        _worldCamera = Camera.main;
    }

    private void Update()
    {
        if (_unit.IsDestroyed() == false)
        {
            float x = _unit.transform.position.x;
            float y = _unit.transform.position.y;
            float z = -10;

            transform.position = new Vector3(x, y, z);
        }
        else
        {
            Vector3 mousePosition = Input.mousePosition;

            float x = _worldCamera.ScreenToViewportPoint(mousePosition).x;
            float y = _worldCamera.ScreenToViewportPoint(mousePosition).y;
            float z = -10;

            transform.position = new Vector3(x, y, z);
        }
    }
}
