using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TurningKnob : ComplexSwitch
{
    public float minRotation;
    public float maxRotation;

    protected override void UpdateSlider()
    {
        normalizedValue = value / (maxValue - minValue);
        float angle = Mathf.Lerp(minRotation, maxRotation, normalizedValue);
        transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0)); 
    }

    protected override float UpdateValue(Vector2 delta)
    {
        return delta.x * sensitivity;
    }
}
