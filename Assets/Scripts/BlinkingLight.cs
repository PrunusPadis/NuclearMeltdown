using UnityEngine;

public class BlinkingLight : MonoBehaviour
{

    public Light targetLight;
    public AnimationCurve blinkCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private float timer = 0f;

    void Start()
    {
        if (targetLight == null)
            targetLight = GetComponent<Light>();
    }

    void Update()
    {
        

        float curveValue = blinkCurve.Evaluate(Time.time);
        targetLight.intensity = curveValue;
    }
}
