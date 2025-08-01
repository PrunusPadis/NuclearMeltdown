using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

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
    ScenarioLevel currentLevel;
    float timeLeft;


    private void Start()
    {
        currentLevel = scenario[levelIndex]; //for easy testing
        timeLeft = currentLevel.dialogue.duration;

    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            CheckScenarioRequirments();
        }
        

    }

    private void CheckScenarioRequirments()
    {
        if (IsComplited(currentLevel.requirment))
        {
            
            levelIndex++;
            Debug.Log("Next level " + levelIndex);
            if (levelIndex >= scenario.Count)
            {
                GameEnd();
                return;
            }
            
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
        timeLeft = currentLevel.dialogue.duration;
    }
}
