using Assets.Scripts.Skills;
using Assets.Scripts.Skills.SkillState;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(LifeStillStateMachine))]
public class SkillUIText : MonoBehaviour
{
    [SerializeField] private TMP_Text _textState;
    [SerializeField] private TMP_Text _textTime;

    private SkillStateType _stateType;

    private void Start()
    {
        SetTimeText("");
    }

    public void SetStateText(string text)
    {
        _textState.text = text;
    }

    public void SetTimeText(string text)
    {
        _textTime.text = text;
    }
}
