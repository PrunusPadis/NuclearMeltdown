
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PhysicalSlider : ComplexSwitch, IPointerEnterHandler, IPointerExitHandler
{




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



}
