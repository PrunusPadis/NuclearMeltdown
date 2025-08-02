using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static ScenerioDialogue;
public class ScenarioManager : MonoBehaviour
{
    [Serializable]
    public class ScenarioLevel
    {
        public ScenarioRequirment requirment;
        public ScenerioDialogue dialogue;

    }

    public ReactorInternals reactor;

    public List<ScenarioLevel> scenario = new();


    [SerializeField] int levelIndex = 0;
    int dialoguePart = 0;
    ScenarioLevel currentLevel;
    float timeLeft;


    private void Start()
    {
        
        reactor = ReactorInternals.Instance;
        currentLevel = scenario[levelIndex]; //for easy testing
        timeLeft = currentLevel.dialogue.parts[dialoguePart].duration;

    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
     
        if (timeLeft < 0)
        {
            if (!IsLastPart(currentLevel))
            {
                dialoguePart++;
                PlayScenarioDialogue(currentLevel);
            }
            else
            {
                CheckScenarioRequirments();
            }
           
        }
        

    }

    private bool IsLastPart(ScenarioLevel level)
    {
        return dialoguePart >= level.dialogue.parts.Count - 1;
    }

    private void CheckScenarioRequirments()
    {
        if (IsComplited(currentLevel.requirment))
        {
            
            levelIndex++;
            dialoguePart = 0;
            if (levelIndex >= scenario.Count)
            {
                GameEnd();
                return;
            }
            Debug.Log("Next level " + levelIndex);
            currentLevel = scenario[levelIndex];
            PlayScenarioDialogue(currentLevel);
        }
    }

    private void GameEnd()
    {
        throw new NotImplementedException();
    }

    private bool IsComplited(ScenarioRequirment requirment)
    {
        if (requirment == null) return true;
        return requirment.Check(reactor);
       
    }

    public void PlayScenarioDialogue(ScenarioLevel level)
    {
        var part = level.dialogue.parts[dialoguePart];
        timeLeft = part.duration;
        SubtitleManager.Instance.PlaySubtitles(part.text);
        AudioPlayer.PlayClipAtPoint(this, part.clip, transform.position);
    }
}
