using System;
using UnityEngine;

public class HealKit : MonoBehaviour
{
    public event Action<HealKit> HealGeting;

    public void Get()
    {
        Debug.Log("GET HK");
        Destroy(gameObject);
        HealGeting?.Invoke(this);
    }
}
