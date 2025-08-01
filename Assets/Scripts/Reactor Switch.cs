using UnityEngine;

public class ReactorSwitch : MonoBehaviour
{
     ReactorInternals reactor;

    public ReactorInternals.ReactorValueType valueType;
    public int index = 0;   //for rods or etc

    private void Start()
    {
        reactor = ReactorInternals.Instance;
    }

    public void ApplyChange(float f)
    {
        switch (valueType)
        {
            case ReactorInternals.ReactorValueType.Power:
                reactor.powerLevel = f;
                break;
            case ReactorInternals.ReactorValueType.Pump:
                reactor.pumpPower = f;
                break;
            case ReactorInternals.ReactorValueType.Rods:
                reactor.controlRods[index] = f;
                break;
            case ReactorInternals.ReactorValueType.Temperature:
                Debug.LogWarning("Cannot change reactor temperature from switch!");
                break;
        }
    }
}