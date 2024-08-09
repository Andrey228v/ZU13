using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(BoxCollider2D))]
public class Spawner : MonoBehaviour
{
    [Min(0.1f)][SerializeField] private float _timeSpawn;
    [SerializeField] private Coin _objectPrefab;

    private bool _isSpawn = true;

    private ObjectPool<Coin> Pool { get; set; }

    private void Start()
    {
        Pool = new ObjectPool<Coin>(CreateObject);

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeSpawn);

        while (_isSpawn)
        {
            Coin spawnerObject = Pool.Get();

            spawnerObject.gameObject.SetActive(true);

            Vector3 position = GetSpawnPosition();

            spawnerObject.transform.position = position;

            spawnerObject.SetSpawner(this);

            yield return wait;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 bounds = GetComponent<BoxCollider2D>().bounds.extents;

        float x = UnityEngine.Random.Range(-bounds.x, bounds.x);
        float y = transform.position.y;
        float z = 0;

        Vector3 position = new Vector3(x, y, z);

        return position;
    }

    private Coin CreateObject()
    {
        return Instantiate(_objectPrefab);
    }

    public void ReturnToPool(Coin coin)
    {
        Pool.Release(coin);
    }
}
