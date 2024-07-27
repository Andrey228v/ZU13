using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> OnCoinInteraction;
    public event Action<Coin> OnCoinGet;

    private void OnDestroy()
    {
        Debug.Log($"Монета уничтожина");
        OnCoinGet(this);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        string layerName = LayerMask.LayerToName( collision.gameObject.layer );

        if(layerName == "Units")
        {
            OnCoinInteraction(this);
        }
    }
}
