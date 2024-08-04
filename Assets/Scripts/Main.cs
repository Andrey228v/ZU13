using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Unit _unit;
    [SerializeField] private List<Enemy> _enemys;

    private void OnEnable()
    {
        _spawner.CoinSpawn += IsCoinSpawn;
    }

    private void OnDisable()
    {
        _spawner.CoinSpawn -= IsCoinSpawn;
    }

    private void IsCoinSpawn(Coin coin)
    {
        coin.OnCoinInteraction += _unit.GetCoin;
        coin.OnCoinGet += IsCoinGet;
    }

    private void IsCoinGet(Coin coin)
    {
        Debug.Log($"Монета уничтожина отписка от события взаимодейсвия с монетой");
        coin.OnCoinInteraction -= _unit.GetCoin;
    }
}
