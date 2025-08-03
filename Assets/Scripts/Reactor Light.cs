using UnityEngine;

public class ReactorLight : MonoBehaviour
{
    public ReactorInternals.ReactorValueType lightType;


    ReactorInternals reactor;
    [SerializeField] GameObject target;

    public bool isAllert;
    private bool isTriggered;
    void Start()
    {
        reactor = ReactorInternals.Instance;
        if (target == null) return;
        target.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered && isAllert) return;

        if (target == null) return;

        if (CheckOn())
        {
            if (isTriggered) return;

            isTriggered = true;
            target.SetActive(true);
            if (isAllert)
            {
                ScenarioManager.Instance.AllertActivated();
            }
        }
        else if(!isAllert && isTriggered)
        {
            isTriggered = false;
            target.SetActive(false);

        }
    }

    private bool CheckOn()
    {
        switch (lightType)
        {
            case ReactorInternals.ReactorValueType.Power:
                return reactor.powerLevel < 10;
            case ReactorInternals.ReactorValueType.Pump:
                break;
            case ReactorInternals.ReactorValueType.Rods:
                break;
            case ReactorInternals.ReactorValueType.CoreTemperature:
                return reactor.temperature > reactor.meltDownTemp * 0.9;
            case ReactorInternals.ReactorValueType.WaterTemperature:
                break;
            case ReactorInternals.ReactorValueType.WaterFlow:
                break;
            case ReactorInternals.ReactorValueType.Cram:
                break;
            case ReactorInternals.ReactorValueType.WaterPressure:
                break;
            case ReactorInternals.ReactorValueType.Toggle:
                break;
            case ReactorInternals.ReactorValueType.Generator:
                break;
            case ReactorInternals.ReactorValueType.AutomaticRod:
                break;
            case ReactorInternals.ReactorValueType.SteaFlow:
                break;
            case ReactorInternals.ReactorValueType.SteamPressure:
                break;
            case ReactorInternals.ReactorValueType.SteamTemperature:
                break;
            case ReactorInternals.ReactorValueType.CondenseFlow:
                break;
            case ReactorInternals.ReactorValueType.CondensePressure:
                break;
            case ReactorInternals.ReactorValueType.CondenseTemperature:
                break;
            case ReactorInternals.ReactorValueType.CondensePump:
                break;
            case ReactorInternals.ReactorValueType.EmergencyPump:
                break;
            case ReactorInternals.ReactorValueType.AutomaticRodEnabled:
                break;
            case ReactorInternals.ReactorValueType.CramEnabled:
                return !reactor.cramEnabled;
            case ReactorInternals.ReactorValueType.Generator1:
                return reactor.generator1;    
            case ReactorInternals.ReactorValueType.Generator2:
                return reactor.generator2;
        }
        Debug.LogWarning("Not implemented");
        return false;
    }
}