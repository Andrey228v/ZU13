using Assets.Scripts.Skills;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(LifeStillStateMachine))]
public class SkillUIText : MonoBehaviour
{
    [SerializeField] private TMP_Text _ui;

    private ISkillStateMachine _skillStateMachine;

    private void Awake()
    {
        _skillStateMachine = GetComponent<LifeStillStateMachine>(); 
    }

    private void Start()
    {
        _skillStateMachine.ChangedState += SetText;
        _skillStateMachine.UsingState.ChangingTime += SetText;
        _skillStateMachine.CooldownState.ChangingTime += SetText;
    }

    private void OnDestroy()
    {
        _skillStateMachine.ChangedState -= SetText;
        _skillStateMachine.UsingState.ChangingTime -= SetText;
        _skillStateMachine.CooldownState.ChangingTime -= SetText;
    }

    private void SetText(string text)
    {
        _ui.text = text;
    }
}
