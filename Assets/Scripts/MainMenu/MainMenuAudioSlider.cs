using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MainMenuAudioSlider : MonoBehaviour
{
    public AudioMixer _mainMixer;
    public AudioMixerGroup audioMixerGroup;
    public PhysicalSlider audioSlider;
    //public string audioMixerGroup;

    public float value;
    public float maxValue;
    public float minValue;

    public float maxRotation;
    public float minRotation;

    public int targetIndex;

    public Transform target;

    //private void Start()
    //{
    //    _mainMixer = audioMixerGroup.audioMixer;

    //    //InitializeAudioSettings();
    //    foreach (var param in _mainMixer.FindMatchingGroups(string.Empty))
    //    {
    //        Debug.Log("Exposed Parameter: " + param.name);
    //    }

    //    Debug.Log("_mainMixer.name: " + _mainMixer.name);

    //    var parameters = _mainMixer.GetFloat("Master", out float value);
    //    Debug.Log("Parameter found: " + parameters + " value: " + value);

    //    var parameters2 = _mainMixer.GetFloat("MasterVolume", out float value2);
    //    Debug.Log("Parameter found: " + parameters2 + " value: " + value2);

    //    var parameters3 = _mainMixer.GetFloat("Volume", out float value3);
    //    Debug.Log("Parameter found: " + parameters3 + " value: " + value3);
    //}




    //public void InitializeAudioSettings()
    //{
    //    _mainMixer = Resources.Load<AudioMixer>("NewAudioMixer");
    //}

    public virtual void UpdateAudioValue()
    {
        float normalizedValue = value / (maxValue - minValue);

        float dbVolume = Mathf.Log10(normalizedValue) * 20;
        if (value == 0.0f) dbVolume = -80.0f;
        //Debug.Log("audioMixerGroup: " + audioMixerGroup);
        //_mainMixer.SetFloat(audioMixerGroup.name+"Volume", dbVolume);
        _mainMixer.SetFloat(audioMixerGroup.name, dbVolume);
    }

    protected virtual void UpdateMeterVisual()
    {
        float normalizedValue = value / (maxValue - minValue);

        float normalizedRotation = normalizedValue / (maxRotation - minRotation);

        float angle = Mathf.Lerp(minRotation, maxRotation, normalizedRotation);
        target.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }

    public void ApplyChange(float f)
    {
        value = f;
        UpdateAudioValue();
        UpdateMeterVisual();
    }

    //private void Update()
    //{
    //    UpdateAudioValue();
    //    UpdateMeterVisual();
    //}

#if UNITY_EDITOR
    void OnValidate()
    {
        UpdateMeterVisual();
    }


#endif

}

