using UnityEngine;

namespace Assets.Scripts.Service.Taker
{
    public class Taker: MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out ITakerObject item))
            {
                if (item is HealKit)
                {
                    HealKit healKit = collision.gameObject.GetComponent<HealKit>();
                    item.TakeObject(TakeUnitTaker());
                }

                if (item is Coin)
                {
                    item.TakeObject(TakeUnitTaker());
                }
            }
        }

        private Player TakeUnitTaker()
        {
            return gameObject.GetComponent<Player>();
        }
    }
}
