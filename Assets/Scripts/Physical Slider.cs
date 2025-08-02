
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PhysicalSlider : ComplexSwitch, IPointerEnterHandler, IPointerExitHandler
{


    public UnityEvent PointerEntered = new();
    public UnityEvent PointerExited = new();

    public Transform maxPosAnchor;
    public Transform minPosAnchor;

    protected override void UpdateSlider()
    {
        normalizedValue = value / (maxValue - minValue);
        transform.position = Vector3.Lerp( minPosAnchor.transform.position,maxPosAnchor.transform.position, normalizedValue);
    }
    protected override float UpdateValue(Vector2 delta)
    {
        return delta.y * sensitivity; 
    }



    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("PhysicalSlider OnPointerEnter");
        PointerEntered.Invoke();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("PhysicalSlider OnPointerExit");
        PointerExited.Invoke();
    }
}
