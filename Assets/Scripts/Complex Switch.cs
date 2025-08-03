using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class ComplexSwitch : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnActivate = new();
    public UnityEvent OnDeactivate = new();

    public UnityEvent PointerEntered = new();
    public UnityEvent PointerExited = new();
    public UnityEvent<float> OnValueChanged = new();

    public float maxValue = 1;
    public float minValue = 0;
    public float value = 0;

    public float sensitivity = 1;

    protected Vector2 screenCursorPos;
    protected bool activated;
    protected float normalizedValue;
    protected float achorValue; //set when clicked
    protected float lastFrameValue;

    public Material defaultMaterial;
    public Material highlightMaterial;

    public MeshRenderer meshRenderer;

    [Header("Audio")]
    public AudioClip audioClip;
    public float audioInterval;
    public float audioTreshold = 1;
    public float volume = 1;
    protected float intervalValue;
    public virtual void OnPointerDown(PointerEventData eventData)
    {


        OnActivate.Invoke();
        screenCursorPos = Mouse.current.position.ReadValue();
        activated = true;
        achorValue = value;
    }


    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        PointerEntered.Invoke();
        meshRenderer.material = highlightMaterial; 
        transform.localScale = Vector3.one * 1.1f; //TODO feedback
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one; //TODO feedback
        meshRenderer.material = defaultMaterial;
        PointerExited.Invoke();
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
            intervalValue -= Time.deltaTime;
            var delta = Mouse.current.position.ReadValue() - screenCursorPos;
            float valueDelta = UpdateValue(delta);
            
            value = achorValue + valueDelta;
            if (value > maxValue) value = maxValue;
            if (value < minValue) value = minValue;
            if(valueDelta != 0)
            {
                OnValueChanged.Invoke(value);
                if(intervalValue < 0 && Mathf.Abs(value- lastFrameValue) > audioTreshold)
                {
                    intervalValue = audioInterval;
                    AudioPlayer.PlayClipAtPoint(this, audioClip, transform.position, volume);
                }
            }
            
            UpdateSlider();
            lastFrameValue = value;
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
