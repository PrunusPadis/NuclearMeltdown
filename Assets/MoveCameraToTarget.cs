using UnityEngine;
using UnityEngine.Events;

public class MoveCameraToTarget : MonoBehaviour
{
    private float minDistance = 0.05f;
    public Camera camera;
    public bool active = true;

    public float moveSpeed = 1f;
    public UnityEvent OnCameraEnd = new();
    void Update()
    {
        if (!active)
        {
            return;
        }

        if (Vector3.Distance(camera.transform.position, transform.position) > minDistance)
        {
            camera.transform.position = Vector3.MoveTowards(
                camera.transform.position,
                transform.position,
                moveSpeed * Time.deltaTime);
        } else
        {
            OnCameraEnd.Invoke();
            active = false;
        }
    }
}
