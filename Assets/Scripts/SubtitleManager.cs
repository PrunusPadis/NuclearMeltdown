using System;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{

    public static SubtitleManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }

    public static void PlaySubtitles(SubtitleText subtitleText)
    {

    }
}

[Serializable]
public class SubtitleText
{
    [SerializeField] public string text;
}
