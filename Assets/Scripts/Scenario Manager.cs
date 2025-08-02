using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ScenerioDialogue;
public class ScenarioManager : MonoSingleton<ScenarioManager>
{
    [Serializable]
    public class ScenarioLevel
    {
        public ScenarioRequirment requirment;
        public ScenerioDialogue dialogue;
    }

    public AudioClip BoomClip;
    public AudioClip CramClip;

    public ReactorInternals reactor;

    public List<ScenarioLevel> scenario = new();

    public bool GameOver;
    public float resetTime = 5;
    [SerializeField] int levelIndex = 0;
    int dialoguePart = 0;
    ScenarioLevel currentLevel;
    float timeLeft;


    private void Start()
    {
        GameOver = false;
        reactor = ReactorInternals.Instance;
        currentLevel = scenario[levelIndex]; //for easy testing
        timeLeft = currentLevel.dialogue.parts[dialoguePart].duration;

    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (GameOver)
        {
            resetTime -= Time.deltaTime;
            if (resetTime < 0)
            {
                SceneManager.LoadScene("MainMenu");
            }

        }

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

    public void CramActivated()
    {
        AudioPlayer.PlayClipAtPoint(this, CramClip, transform.position);
        GameOver = true;
    }

    private void GameEnd()
    {
        GameOver = true;
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
        if(part.effect != null)
        {
            part.effect.TriggerEffect();
        }
    }
}
