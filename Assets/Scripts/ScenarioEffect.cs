using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioEffect1", menuName = "Scriptable Objects/ScenarioEffect1")]
public class ScenarioEffect : ScriptableObject
{

    [SerializeField] float time;
    [SerializeField] float magnitude;
    public void TriggerEffect() //expand this more effects!
    {
        ScreenShake.Instance.Shake(time, magnitude);
    }
}
