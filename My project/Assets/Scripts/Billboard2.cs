using UnityEngine;

public class Billboard2 : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (cam == null) return;

        transform.forward = cam.transform.forward;
    }
}