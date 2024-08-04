using UnityEngine;

public class Unit : MonoBehaviour
{
    public void GetCoin(Coin coin)
    {
        Destroy(coin.gameObject);
    }
}
