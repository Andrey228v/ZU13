using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> OnCoinGet;

    private Spawner _spawner;

    public void Get()
    {
        gameObject.SetActive(false);
        _spawner.ReturnToPool(this);
    }

    public void SetSpawner(Spawner spawner)
    {
        _spawner = spawner;
    }
}
