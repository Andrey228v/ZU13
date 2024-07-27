using UnityEngine;

public class Unit : MonoBehaviour
{
    public void GetCoin(Coin coin)
    {
        coin.GetComponent<SpriteRenderer>().color = Color.red;
        Destroy(coin.gameObject);
    }
}
