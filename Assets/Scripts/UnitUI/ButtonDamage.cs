using Assets.Scripts.UnitUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDamage : ButtonUI, IPointerUpHandler
{
    private int _damage = 10;

    public override void Awake()
    {
        base.Awake();

        Button.image.color = Color.red;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Health.TakeDamage(_damage);
    }
}
