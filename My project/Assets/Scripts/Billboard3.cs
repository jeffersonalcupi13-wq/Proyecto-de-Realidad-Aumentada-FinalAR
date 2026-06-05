using UnityEngine;

public class Billboard3 : MonoBehaviour
{
    public Camera arCamera;

    private void LateUpdate()
    {
        if (arCamera == null)
            return;

        Vector3 targetPosition = arCamera.transform.position;

        targetPosition.y = transform.position.y;

        transform.LookAt(targetPosition);

        transform.Rotate(0, 180f, 0);
    }
}