using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HideManual : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public UnityEvent OnActivate = new();
    public bool active = false;

    public void ToggleHideManual()
    {
        active = !active;
        GetComponent<BoxCollider>().enabled = active;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!active) return;

        ToggleHideManual();
        OnActivate.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}
