using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GenericSwitch : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnActivate = new();
    public UnityEvent OnDeactivate = new();
    public UnityEvent<bool> OnValueChanged = new();
    public UnityEvent PointerEntered = new();
    public UnityEvent PointerExited = new();
    public bool switchable = false;

    public float maxValue = 1;
    public float minValue = 0;
    public bool isOn = false;

    public Material defaultMaterial;
    public Material highlightMaterial;

    public MeshRenderer meshRenderer;

    public Vector3 onPos;
    public Vector3 offPos;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("GenericSwitch OnPointerDown");
        OnActivate.Invoke();
        isOn = !isOn;
        OnValueChanged.Invoke(isOn);
        UpdateSwitch();
    }


    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("GenericSwitch OnPointerEnter");
        meshRenderer.material = highlightMaterial;
        transform.localScale = Vector3.one * 1.1f; //TODO feedback
        PointerEntered.Invoke();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("GenericSwitch OnPointerExit");
        transform.localScale = Vector3.one; //TODO feedback
        meshRenderer.material = defaultMaterial;
        PointerExited.Invoke();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("GenericSwitch OnPointerUp");
        OnDeactivate.Invoke();
        if (!switchable)
        {
            isOn = !isOn;
            OnValueChanged.Invoke(isOn);
        }
        UpdateSwitch();
    }


    protected virtual void UpdateSwitch()
    {
        if (isOn)
        {
            transform.localPosition = onPos;
        }
        else
        {
            transform.localPosition = offPos;
        }
       
    }


#if UNITY_EDITOR
    void OnValidate()
    {

        UpdateSwitch();
    }

#endif
}
