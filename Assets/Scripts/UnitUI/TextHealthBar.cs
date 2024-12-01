using Assets.Scripts.Service.Health;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(HealthUnits))]
public class TextHealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;

    private HealthUnits _health;
    private string _textHP;

    private void Awake()
    {
        _health = GetComponent<HealthUnits>();

        _health.Damaged += ChangeData;
        _health.Healed += ChangeData;
    }

    private void OnDestroy()
    {
        _health.Damaged -= ChangeData;
        _health.Healed -= ChangeData;
    }

    private void Start()
    {
        _textHP = "HP: " + _health.Amount + "/" + _health.MaxAmount;
        _healthText.text = _textHP;
    }

    public void ChangeData()
    {
        _textHP = "HP: " + _health.Amount + "/" + _health.MaxAmount;
        _healthText.text = _textHP;
    } 
}
