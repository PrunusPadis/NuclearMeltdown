using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MainMenuAudioSlider : MonoBehaviour
{
    public AudioMixer _mainMixer;
    public AudioMixerGroup audioMixerGroup;
    public PhysicalSlider audioSlider;
    //public string audioMixerGroup;





    public virtual void UpdateAudioValue(float value)
    {

        float dbVolume = Mathf.Log10(value) * 20;
        if (value == 0.0f) dbVolume = -80.0f;
        //Debug.Log("audioMixerGroup: " + audioMixerGroup);
        //_mainMixer.SetFloat(audioMixerGroup.name+"Volume", dbVolume);
        _mainMixer.SetFloat(audioMixerGroup.name, dbVolume);
    }


}

