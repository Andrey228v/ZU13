using UnityEngine.UI;
using UnityEngine;
using Assets.Scripts.UnitUI;

public class SmothSliderHealthBar : SliderUI
{
    [SerializeField] private int _speedReduction;

    public override void UpdateAmount()
    {
        base.UpdateAmount();
        Slider.value = Mathf.MoveTowards(Slider.value, Health.Amount, _speedReduction * Time.deltaTime);
    }
}
