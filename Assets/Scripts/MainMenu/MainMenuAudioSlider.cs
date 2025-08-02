using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MainMenuAudioSlider : MonoBehaviour
{
    private AudioMixer _mainMixer;
    public PhysicalSlider audioSlider;
    public string audioMixerGroup;

    public float value;
    public float maxValue;
    public float minValue;

    public float maxRotation;
    public float minRotation;

    public int targetIndex;

    public Transform target;

    private void Start()
    {
        InitializeAudioSettings();
    }

    public void InitializeAudioSettings()
    {
        _mainMixer = Resources.Load<AudioMixer>("NewAudioMixer");
    }

    public virtual void UpdateAudioValue()
    {
        _mainMixer.SetFloat(audioMixerGroup, value);
    }

    protected virtual void UpdateMeterVisual()
    {
        float normalizedValue = value / (maxValue - minValue);

        float angle = Mathf.Lerp(minRotation, maxRotation, normalizedValue);
        target.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }

    private void Update()
    {
        UpdateAudioValue();
        UpdateMeterVisual();

    }
#if UNITY_EDITOR
    void OnValidate()
    {
        UpdateMeterVisual();
    }


#endif

}

