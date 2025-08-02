using TMPro;
using UnityEngine;

public class TextDisplayer : MonoBehaviour
{
    public TextMeshPro textMeshPro;

    public void SetText(string text)
    {
        textMeshPro.text = text;
    }

    public void ClearText()
    {
        textMeshPro.text = "";
    }
}
