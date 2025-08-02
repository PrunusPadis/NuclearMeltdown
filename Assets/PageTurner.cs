using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PageTurner : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public UnityEvent OnActivate = new();
    public bool active = false;

    public void setEnabled(bool enabled)
    {
        active = enabled;
        GetComponent<BoxCollider>().enabled = active;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!active) return;
        OnActivate.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}
