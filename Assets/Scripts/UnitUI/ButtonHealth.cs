using Assets.Scripts.UnitUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHealth : ButtonUI, IPointerUpHandler
{
    private int _healthPoints = 10;
    
    public override void Awake()
    {
        base.Awake();

        Button.image.color = Color.green;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Health.TryTakeHealing(_healthPoints);
    }
}
