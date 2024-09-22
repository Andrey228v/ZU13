using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Player _unit;
    
    private void Update()
    {
        float x = _unit.transform.position.x;
        float y = _unit.transform.position.y;
        float z = -10;

        transform.position = new Vector3(x, y, z);
    }
}
