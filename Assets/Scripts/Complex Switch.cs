using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class ComplexSwitch : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnActivate = new();
    public UnityEvent OnDeactivate = new();
    public UnityEvent<float> OnValueChanged = new();

    public float maxValue = 1;
    public float minValue = 0;
    public float value = 0;

    public float sensitivity = 1;

    protected Vector2 screenCursorPos;
    protected bool activated;
    protected float normalizedValue;
    protected float achorValue; //set when clicked

    public virtual void OnPointerDown(PointerEventData eventData)
    {


        OnActivate.Invoke();
        screenCursorPos = Mouse.current.position.ReadValue();
        activated = true;
        achorValue = value;
    }


    public virtual void OnPointerEnter(PointerEventData eventData)
    {

        transform.localScale = Vector3.one * 1.1f; //TODO feedback
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one; //TODO feedback
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        OnDeactivate.Invoke();

        activated = false;
    }

    void Update()
    {
        if (activated)
        {
            var delta = Mouse.current.position.ReadValue() - screenCursorPos;
            float valueDelta = UpdateValue(delta);
            value = achorValue + valueDelta;
            if (value > maxValue) value = maxValue;
            if (value < minValue) value = minValue;
            if(valueDelta != 0)
            {
                OnValueChanged.Invoke(value);
            }
            
            UpdateSlider();

        }
    }

    protected virtual void UpdateSlider()
    {
        Debug.LogError("Missing implementation" + this);
    }
    protected virtual float UpdateValue(Vector2 delta)
    {

        Debug.LogError("Missing implementation" + this);
        return 1;
    }

#if UNITY_EDITOR
    void OnValidate()
    {

        UpdateSlider();
    }

#endif
}
