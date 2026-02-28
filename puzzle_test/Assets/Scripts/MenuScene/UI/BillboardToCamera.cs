using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (!cam) return;

        transform.rotation = Quaternion.LookRotation(
            transform.position - cam.transform.position,
            Vector3.up
        );
    }
}
