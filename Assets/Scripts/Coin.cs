using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> CoinGeting;

    public void Get()
    {
        gameObject.SetActive(false);
        CoinGeting?.Invoke(this);
    }
}
