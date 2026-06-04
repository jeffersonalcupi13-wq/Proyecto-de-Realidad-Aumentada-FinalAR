using UnityEngine;

public class Billboard3 : MonoBehaviour
{
    public Camera arCamera;

    private void LateUpdate()
    {
        if (arCamera == null)
            return;

        transform.LookAt(arCamera.transform);

        transform.Rotate(0, 180f, 0);
    }
}