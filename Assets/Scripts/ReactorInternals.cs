using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ReactorInternals : MonoSingleton<ReactorInternals>
{
    public enum ReactorValueType { Power, Pump, Rods, CoreTemperature, WaterTemperature,
                                    WaterFlow, Cram, WaterPressure,Toggle, Generator, AutomaticRod,
                                    SteaFlow, SteamPressure, SteamTemperature,
                                     CondenseFlow, CondensePressure, CondenseTemperature, CondensePump,
            EmergencyPump
                                        };
    [Header("Core")]

    public float powerLevel;
    
    public float temperature;

    public float maxTemp;
    public float controlTemp;

    [Header("Rods")]

    public float controlRodModifier = 10;

    public List<float> controlRods;
    public List<float> automaticRods;

    public float automaticRodSpeed = 1;
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

    

    private void Update()
    {
        if (ScenarioManager.Instance.GameOver)
        {
            this.enabled = false;
        }

        float newPower = 0;
        foreach (var controlRod in controlRods)
        {
            newPower += Mathf.Pow(controlRod * controlRodModifier,2);
        }
        foreach (var controlRod in automaticRods)
        {
            //newPower += Mathf.Pow(controlRod * controlRodModifier, 2);
            
        }
        

        powerLevel = newPower;

        //water
        totalPumpPower = 0;
        foreach(var f in circulationPumps)
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
            temperature = powerLevel  * temperatureModifier; //aka meltdow etc
            cramActivated = true;
            ScenarioManager.Instance.CramActivated();
        }

        //steam
        steamPressure = temperature * steamPressureMod;
        steamFlow = steamPressure * steamFlowMod;
        steamTemperature = temperature * steamTempMod;

        //water cooldown
        waterTemperature = steamTemperature - (condenseTemperature * condenseFlow * condenseMod);
        waterPressure = waterTemperature * waterPressureMod;
        waterFlow = waterPressure * waterFlowMod;

        condenseTemperature = condenseFlow * condenseWarmingMod; //TODO WARMING
        condenseFlow = condensePump;
        condensePressure = condenseTemperature * condenseFlow;




    }



}
