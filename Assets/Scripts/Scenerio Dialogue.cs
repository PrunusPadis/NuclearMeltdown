using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScenerioDialogue", menuName = "Scriptable Objects/ScenerioDialogue")]
public class ScenerioDialogue : ScriptableObject
{
    public List<DialoguePart> parts;
    [Serializable]
    public class DialoguePart
    {
        public string text;
        public AudioClip clip;

        public float duration;

        public ScenarioEffect effect;
    }
}
