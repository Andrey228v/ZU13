using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(BoxCollider2D))]
public class CoinSpawner : MonoBehaviour
{
    [Min(0.1f)][SerializeField] private float _timeSpawn;
    [SerializeField] private Coin _objectPrefab;

    private bool _isSpawn = true;
    private ObjectPool<Coin> _pool;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        _pool = new ObjectPool<Coin>(CreateObject);

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeSpawn);

        while (_isSpawn)
        {
            Coin spawnerObject = _pool.Get();

            spawnerObject.Collected += ReturnToPool;

            Vector3 position = GetSpawnPosition();

            spawnerObject.transform.position = position;
            spawnerObject.gameObject.SetActive(true);

            yield return wait;
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

    private Coin CreateObject()
    {
        return Instantiate(_objectPrefab);
    }

    private void ReturnToPool(Coin coin)
    {
        coin.Collected -= ReturnToPool;
        _pool.Release(coin);
    }
}
