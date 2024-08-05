using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private const string CollisionLayerNameUnit = "Units";

    public event Action<Coin> OnCoinInteraction;
    public event Action<Coin> OnCoinGet;

    private void OnDestroy()
    {
        OnCoinGet(this);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        string layerName = LayerMask.LayerToName( collision.gameObject.layer );

        if(layerName == CollisionLayerNameUnit)
        {
            OnCoinInteraction(this);
        }
    }
}
