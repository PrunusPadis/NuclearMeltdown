using TMPro;
using UnityEngine;

public class ToolTipManager : MonoBehaviour //definetly not subtitleManager
{

    public static ToolTipManager Instance;

    public GameObject SubtitleText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }

    public void PlaySubtitles(string text)
    {
        SubtitleText.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void ClearSubtitles()
    {
        SubtitleText.GetComponent<TextMeshProUGUI>().text = "";
    }
}
