using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LabelHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    string text;
    void Awake()
    {
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>().text;


    }
    //public Vector3 labalePosition ; // Scale on hover
    //private Vector3 originalPosition;

    //void Start()
    //{
    //    originalPosition = transform.position;
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTipManager.Instance.PlaySubtitles(text);
        //transform.position = labalePosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipManager.Instance.ClearSubtitles();
        // transform.position = originalPosition;
    }
}
