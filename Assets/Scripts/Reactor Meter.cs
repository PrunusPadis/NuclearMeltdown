using UnityEngine;

public class ReactorMeter : MonoBehaviour
{
    public ReactorInternals reactor;

    public ReactorInternals.ReactorValueType meterType;

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
                value = reactor.pumpPower;
                break;
            case ReactorInternals.ReactorValueType.Rods:
                value = reactor.controlRods[targetIndex];
                break;
            case ReactorInternals.ReactorValueType.Temperature:
                value = reactor.temperature;
                break;
           
        }

        if (value > maxValue) value = maxValue;
        if (value < minValue) value = minValue;

    }
    protected virtual void UpdateMeterVisual()
    {
        float normalizedValue = value / (maxValue - minValue);
       
        float angle = Mathf.Lerp(minRotation, maxRotation, normalizedValue);
        target.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));

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

