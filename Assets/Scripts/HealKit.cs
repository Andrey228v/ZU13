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

    public void Get(GameObject taker)
    {
        if (taker.TryGetComponent(out IHealthTaker healthTaker))
        {
            healthTaker.TakeHealth(this);
        }
    }
}
