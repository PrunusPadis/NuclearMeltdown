using UnityEngine;

public class MoveCameraToTarget : MonoBehaviour
{
    private float minDistance = 0.05f;
    public Camera camera;
    public bool active = true;

    public float moveSpeed = 1f;

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
            active = false;
        }
    }
}
