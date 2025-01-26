using Assets.Scripts.Skills;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(LifeStillStateMachine))]
public class SkillUIText : MonoBehaviour
{
    [SerializeField] private TMP_Text _ui;
    
    private LifeStillStateMachine _skillStateMachine;
    private string _text;

    private void Awake()
    {
        _skillStateMachine = GetComponent<LifeStillStateMachine>();
        _skillStateMachine.ChangedState += SetText;
    }

    private void Start()
    {
        _skillStateMachine.UsingState.ChangingTime += UpdateText;
        _skillStateMachine.CooldownState.ChangingTime += UpdateText;
    }

    private void OnDisable()
    {
        _skillStateMachine.ChangedState -= SetText;
        _skillStateMachine.UsingState.ChangingTime -= UpdateText;
        _skillStateMachine.CooldownState.ChangingTime -= UpdateText;
    }

    private void UpdateText(string text)
    {
        _ui.text = text;
    }

    private void SetText(string text)
    {
        _ui.text = text;
        _text = text;
    }
}
