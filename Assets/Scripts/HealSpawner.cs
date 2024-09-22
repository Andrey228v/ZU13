using UnityEngine;

public class HealSpawner : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private HealKit _objectPrefab;

    private bool _isSpawned = true;
    private int _spawnCount = 0;
    private Vector3 _positionSpawn;
    private HealKit _healKit;

    private void Start()
    {
        while(_isSpawned)
        {
            if (_spawnCount < _count) 
            {
                if(TryGetSpawnPosition(out _positionSpawn))
                {
                    _healKit = CreateObject();
                    _healKit.transform.position = _positionSpawn;
                    _spawnCount++;
                }
            }

            if (_spawnCount == _count) 
            {
                _isSpawned = false;
            }
        }
    }

    private bool TryGetSpawnPosition(out Vector3 position)
    {
        bool isPositionFind = false;

        Vector2 bounds = GetComponent<BoxCollider2D>().bounds.extents;

        float x = UnityEngine.Random.Range(-bounds.x, bounds.x);
        float y = UnityEngine.Random.Range(-bounds.y, bounds.y);
        float z = 1;

        Vector2 spawnPoint = new Vector2(x, y);
        _objectPrefab.TryGetComponent(out BoxCollider2D _prefabCollider);

        Vector2 size = _prefabCollider.bounds.size;

        Collider2D collider = Physics2D.OverlapBox(spawnPoint, size, 0);

        position = spawnPoint;

        if (collider == null)
        {
            isPositionFind = true;
        }

        return isPositionFind;
    }

    private HealKit CreateObject()
    {
        return Instantiate(_objectPrefab);
    }
}
