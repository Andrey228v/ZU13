using Assets.Scripts.Skills;
using TMPro;
using UnityEngine;

public class SkillUIText : MonoBehaviour
{
    [SerializeField] private TMP_Text _ui;
    
    private ISkillView _skillView;

    private void Awake()
    {
        _skillView = GetComponent<ISkillView>();
        _skillView.ChangedState += SetText;
    }

    private void OnEnable()
    {
        _skillView.ChangedState -= SetText;
    }

    private void SetText(string text)
    {
        Debug.Log($"TEST: {text}");
        _ui.text = text;
    }
}
