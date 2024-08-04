using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    
    void Update()
    {
        float x = _unit.transform.position.x;
        float y = _unit.transform.position.y;
        float z = -10;

        transform.position = new Vector3(x, y, z);
    }
}
