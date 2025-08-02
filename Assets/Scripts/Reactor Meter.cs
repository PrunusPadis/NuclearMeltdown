using Unity.VisualScripting;
using UnityEngine;

public class ReactorMeter : MonoBehaviour
{
    public ReactorInternals reactor;

    public ReactorInternals.ReactorValueType meterType;

    public enum RollAxis { x,y,z};
    public RollAxis axis;

    public float value;
    public float maxValue;
    public float minValue;

    public float maxRotation;
    public float minRotation;

    public int targetIndex;

    public Transform target;
    private void Start()
    {
        reactor = ReactorInternals.Instance;
    }
    public virtual void UpdateMeterValue()
    {
        switch (meterType)
        {
            case ReactorInternals.ReactorValueType.Power:
                value = reactor.powerLevel;
                break;
            case ReactorInternals.ReactorValueType.Pump:
                value = reactor.circulationPumps[targetIndex];
                break;
            case ReactorInternals.ReactorValueType.Rods:
                value = reactor.controlRods[targetIndex];
                break;
            case ReactorInternals.ReactorValueType.CoreTemperature:
                value = reactor.temperature;
                break;
            case ReactorInternals.ReactorValueType.WaterTemperature:
                value = reactor.waterTemperature;
                break;
            case ReactorInternals.ReactorValueType.WaterFlow:
                value = reactor.waterFlow;
                break;
            case ReactorInternals.ReactorValueType.WaterPressure:
                value = reactor.waterPressure;
                break;
            case ReactorInternals.ReactorValueType.Generator:

                break;
            case ReactorInternals.ReactorValueType.AutomaticRod:
                value = reactor.automaticRods[targetIndex];
                break;
            case ReactorInternals.ReactorValueType.SteaFlow:
                value = reactor.steamFlow;
                break;
            case ReactorInternals.ReactorValueType.SteamPressure:
                value = reactor.steamPressure;
                break;
            case ReactorInternals.ReactorValueType.SteamTemperature:
                value = reactor.steamTemperature;
                break;
            case ReactorInternals.ReactorValueType.Cram:
                break;
            case ReactorInternals.ReactorValueType.Toggle:
                break;
            case ReactorInternals.ReactorValueType.CondenseFlow:
                value = reactor.condenseFlow;
                break;
            case ReactorInternals.ReactorValueType.CondensePressure:
                value = reactor.condensePressure;
                break;

            case ReactorInternals.ReactorValueType.CondenseTemperature:
                value = reactor.condenseTemperature;
                break;
            case ReactorInternals.ReactorValueType.EmergencyPump:
                value = reactor.emergencyPump;
                break;
            case ReactorInternals.ReactorValueType.CondensePump:
                value = reactor.condensePump;
                break;
        }

        if (value > maxValue) value = maxValue;
        if (value < minValue) value = minValue;

    }
    protected virtual void UpdateMeterVisual()
    {
        float normalizedValue = value / (maxValue - minValue);
       
        float angle = Mathf.Lerp(minRotation, maxRotation, normalizedValue);
        switch (axis)
        {
            case RollAxis.x:
                target.localRotation = Quaternion.Euler(new Vector3(angle, 0, 0));
                break;
            case RollAxis.y:
                target.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
                break;
            case RollAxis.z:
                target.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                break;
            default:
                break;
        }
      

    }

    private void Update()
    {
        UpdateMeterValue();
        UpdateMeterVisual();
      
    }
#if UNITY_EDITOR
    void OnValidate()
    {
        UpdateMeterVisual();
    }


#endif
}

