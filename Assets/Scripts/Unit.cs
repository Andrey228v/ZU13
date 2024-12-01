using Assets.Scripts.Service.Health;
using UnityEngine;

[RequireComponent(typeof(HealthUnits))]
public class Unit : MonoBehaviour
{
    public HealthUnits Health {get; private set;}

    private void Awake()
    {
        Health = GetComponent<HealthUnits>();
    }
}
