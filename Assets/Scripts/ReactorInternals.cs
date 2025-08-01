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





}
