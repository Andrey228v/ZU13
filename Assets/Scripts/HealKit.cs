using Assets.Scripts.Service;
using UnityEngine;

public class HealKit : MonoBehaviour, ITakerObject
{
    [SerializeField] private int _healthPoints;

    public int HealthPoints { get; private set; }

    private void Awake()
    {
        HealthPoints = _healthPoints;
    }

    public void TakeObject(Player player)
    {
        if (player.Health.TryTakeHealing(HealthPoints))
        {
            Destroy(gameObject);
        }
    }
}
