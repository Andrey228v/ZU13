using UnityEngine.UI;
using Assets.Scripts.UnitUI;

public class SliderHealthBar : SliderUI
{
    public override void ChangeData()
    {
        base.ChangeData();

        Slider.value = Health.Amount;
    }
}
