using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HealSpawner : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private HealKit _objectPrefab;

    private bool _isSpawned = true;
    private int _spawnCount = 0;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        for (int i = 0; i < _count; i++)
        {
            Vector3 position = GetSpawnPosition();

            HealKit spawnerObject = CreateObject();
            spawnerObject.transform.position = position;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 bounds = _collider.bounds.extents;

        float x = UnityEngine.Random.Range(-bounds.x, bounds.x);
        float y = transform.position.y;
        float z = 0;

        return new Vector3(x, y, z);
    }

    private HealKit CreateObject()
    {
        return Instantiate(_objectPrefab);
    }
}
