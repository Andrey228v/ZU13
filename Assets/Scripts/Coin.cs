using Assets.Scripts.Service;
using System;
using UnityEngine;

public class Coin : MonoBehaviour, ITakerObject
{
    public event Action<Coin> Collected;

    public void TakeObject(Player player)
    {
        gameObject.SetActive(false);
        Collected?.Invoke(this);
    }
}
