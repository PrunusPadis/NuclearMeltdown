using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ReactorInternals : MonoSingleton<ReactorInternals>
{
    public enum ReactorValueType { Power, Pump, Rods, CoreTemperature, WaterTemperature,
                                    WaterFlow, Cram, WaterPressure,Toggle, Generator, AutomaticRod,
                                    SteaFlow, SteamPressure, SteamTemperature,
                                     CondenseFlow, CondensePressure, CondenseTemperature, CondensePump,
            EmergencyPump, AutomaticRodEnabled, CramEnabled, Generator1, Generator2
    };
    [Header("Core")]

    public float powerLevel;
    
    public float temperature;

    public float maxTemp;
    public float controlTemp;


    public float meltDownTemp;

    [Header("Rods")]

    public float controlRodModifier = 10;

    public List<float> controlRods;
    public List<float> automaticRods;

    public float automaticRodSpeed = 1;
    public float automaticTemp = 400;
    public float automaticTempLow = 350;

    public bool automaticRodsEnabled = true;
    public bool automaticRodsLocked = false;
    [Header("Pumps")]

    public float temperatureModifier = 10;

    public List<float> circulationPumps;
    public float condensePump;
    public float emergencyPump;


    [Header("Water")]

    public float steamPressure;
    public float steamFlow;

   

    public float waterPressure;
    public float waterFlow;

    public float condenseFlow;
    public float condensePressure;

    public float waterTemperature;
    public float steamTemperature;
    public float condenseTemperature;

    public float totalPumpPower;

    public bool steamVent;

    [Header("Water Mods")]

    [SerializeField] float steamMod = 1;
    [SerializeField] float condenseMod = 1;
    [SerializeField] float condenseWarmingMod = 1;
    [SerializeField] float waterPressureMod = 1;
    [SerializeField] float waterFlowMod= 1;
    [SerializeField] float steamPressureMod = 1;
    [SerializeField] float steamFlowMod = 1;
    [SerializeField] float steamTempMod = 1;

    [Header("Cram")]

    public bool cramActivated;
    public bool cramEnabled;

    [Header("Generators")]

    public bool generator1;
    public bool generator2;

    private void Update()
    {
        if (ScenarioManager.Instance.GameOver)
        {
            this.enabled = false;
        }

        float newPower = 0;
        foreach (var controlRod in controlRods)
        {
            newPower += controlRod * controlRodModifier;
        }
        foreach (var aRod in automaticRods)
        {
            newPower += aRod * controlRodModifier;

        }

        AutomaticRods();
        if (steamVent)
        {
            powerLevel = 0;
            
        }
        else
        {
            powerLevel = newPower;
        }
       
        if(powerLevel < 1)
        {
            generator1 = true;
        }
        //water
        totalPumpPower = 0;
        foreach (var f in circulationPumps)
        {
            totalPumpPower += f;
        }
        totalPumpPower += emergencyPump;

        if (totalPumpPower > 0)
        {
            temperature = (powerLevel / totalPumpPower) * temperatureModifier;

        }
        else
        {
            temperature = powerLevel * temperatureModifier; //aka meltdow etc
           
        }
        if(temperature > meltDownTemp)
        {
            cramActivated = true;
            ScenarioManager.Instance.AllertActivated();
        }

        //steam
        if (steamVent)
        {
            steamPressure = 0;
            steamFlow = 0;
           
        }
        else
        {
            steamPressure = temperature * steamPressureMod;
            steamFlow = steamPressure * steamFlowMod;
        }
        
        steamTemperature = temperature * steamTempMod;

        //water cooldown
        waterTemperature = steamTemperature - (condenseTemperature * condenseFlow * condenseMod);
        waterPressure = waterTemperature * waterPressureMod;
        waterFlow = waterPressure * waterFlowMod;

        condenseTemperature = condenseFlow * condenseWarmingMod; //TODO WARMING
        condenseFlow = condensePump;
        condensePressure = condenseTemperature * condenseFlow;




    }

    private void AutomaticRods()
    {
        if (automaticRodsLocked) return;

        if (automaticRodsEnabled)
        {
            if (temperature < automaticTempLow)
            {
                for (int i = 0; i < automaticRods.Count; i++)
                {
                    automaticRods[i] += automaticRodSpeed;
                    if (automaticRods[i] > 20)
                    {
                        automaticRods[i] = 20;
                    }
                }
            }
            else if(temperature > automaticTemp)
            {
                for (int i = 0; i < automaticRods.Count; i++)
                {
                    automaticRods[i] -= automaticRodSpeed;
                    if (automaticRods[i] < 0)
                    {
                        automaticRods[i] = 0;
                    }
                   
                   
                }
            }
        }
        else
        {
            for (int i = 0; i < automaticRods.Count; i++)
            {
                automaticRods[i] -= automaticRodSpeed;
                if (automaticRods[i] < 0)
                {
                    automaticRods[i] = 0;
                }
            }
        }
        
    }

    public void LockAutoRods(bool b)
    {
        automaticRodsLocked = b;
      
    }

    public void DisableAutoRods(bool b)
    {
        automaticRodsEnabled = b;

    }

    public void ManualCram(bool b)
    {
        cramActivated = b;
        if (b)
        {
            ScenarioManager.Instance.AllertActivated();
        }
        
    }

    public void DisableCram(bool b)
    {
        cramEnabled = b;

    }

    public void EnableGenerator1(bool b)
    {
        generator1 = b;

    }
    public void EnableGenerator2(bool b)
    {
        generator2 = b;

    }

    public void SteamVent(bool b)
    {
        steamVent = b;

    }
}
