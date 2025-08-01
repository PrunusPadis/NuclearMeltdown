using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioRequirment", menuName = "Scriptable Objects/ScenarioRequirment")]
public class ScenarioRequirment : ScriptableObject
{
  

    public enum RequirmentComparer { More, Less, Equal };

    public ReactorInternals.ReactorValueType requirmentType;
    public RequirmentComparer comparer;

    public float value;

    internal bool Check(ReactorInternals reactor)
    {
        switch (requirmentType)
        {
            case ReactorInternals.ReactorValueType.Power:
                return Compare(reactor.powerLevel);
            case ReactorInternals.ReactorValueType.Pump:
                return Compare(reactor.pumpPower);
            case ReactorInternals.ReactorValueType.Rods:

                foreach (float f in reactor.controlRods)
                {
                    if (!Compare(f))
                    {
                        return false;
                    }
                }
                return true;
            case ReactorInternals.ReactorValueType.Temperature:
                return Compare(reactor.temperature);
            default:
                Debug.LogWarning("Requirement not implemented " + requirmentType);
                return true;
        }
    }
    bool Compare(float toCompare)
    {
        switch (comparer)
        {
            case RequirmentComparer.More:
                return toCompare > value;

            case RequirmentComparer.Less:
                return toCompare < value;
            case RequirmentComparer.Equal:
                return toCompare == value;
            default:
                Debug.LogWarning("Comparer not implemented " + comparer);
                return true;
        }
    }
}