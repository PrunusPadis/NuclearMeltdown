using System;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{

    public static SubtitleManager Instance;

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
}
