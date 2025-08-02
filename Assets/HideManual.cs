using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HideManual : MonoBehaviour, IPointerUpHandler
{
    public UnityEvent OnActivate = new();
    public bool active = false;

    public void ToggleHideManual()
    {
        active = !active;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!active) return;

        ToggleHideManual();
        OnActivate.Invoke();
    }
}
