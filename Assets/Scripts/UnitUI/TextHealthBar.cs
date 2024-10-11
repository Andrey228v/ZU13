using Assets.Scripts.Service;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(IHealth))]
public class TextHealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;

    private IHealth _health;
    private string _textHP;

    private void Awake()
    {
        _health = GetComponent<IHealth>();

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
