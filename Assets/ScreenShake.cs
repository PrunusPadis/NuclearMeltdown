using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance; // Singleton pattern (optional)

    private Vector3 originalPosition;

    void Awake()
    {
        Instance = this;
        originalPosition = transform.localPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        originalPosition = transform.localPosition; // this works if you dont spam this shake...
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private System.Collections.IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    // for testing
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            ScreenShake.Instance.Shake(1.0f, 0.1f);
        }
    }
}
