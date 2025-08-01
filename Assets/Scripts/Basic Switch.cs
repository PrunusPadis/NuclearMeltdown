using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BasicSwitch : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,  IPointerClickHandler
{
    public UnityEvent<bool> OnSwitch = new();
    public bool isOn = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        isOn = !isOn;
        OnSwitch.Invoke(isOn);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
       transform.localScale = Vector3.one* 1.1f; //TODO feedback
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one; //TODO feedback
    }
}
