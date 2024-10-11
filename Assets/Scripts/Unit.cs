using Assets.Scripts.Service;
using UnityEngine;

[RequireComponent(typeof(IHealth))]
public class Unit : MonoBehaviour
{
    public IHealth Health {get; private set;}

    private void Awake()
    {
        Health = GetComponent<IHealth>();
    }
}
