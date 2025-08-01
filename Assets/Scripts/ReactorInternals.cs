using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ReactorInternals : MonoSingleton<ReactorInternals>
{
    public enum ReactorValueType { Power, Pump, Rods, Temperature };

    public float powerLevel;
    public float temperature;

    public List<float> controlRods;

    public float pumpPower;

    [Header("Mechanic values")]

    public float controlRodModifier = 10;

    public float temperatureModifier = 10;

    private void Update()
    {
        float newPower = 0;
        foreach (var controlRod in controlRods)
        {
            newPower += Mathf.Pow(controlRod * controlRodModifier,2);


        }
        powerLevel = newPower;

        if(pumpPower > 0)
        {
            temperature = (powerLevel / pumpPower) * temperatureModifier;
        }
        else
        {
            temperature = powerLevel  * temperatureModifier; //aka meltdow etc
        }
            
    }



}
