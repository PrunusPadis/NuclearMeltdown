using UnityEngine;

[CreateAssetMenu(fileName = "ScenerioDialogue", menuName = "Scriptable Objects/ScenerioDialogue")]
public class ScenerioDialogue : ScriptableObject
{
    public string text;
    public AudioClip clip;

    public float duration;
}
