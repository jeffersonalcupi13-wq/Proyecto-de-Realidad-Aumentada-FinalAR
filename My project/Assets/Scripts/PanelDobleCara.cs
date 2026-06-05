using UnityEngine;

public class PanelDobleCara : MonoBehaviour
{
    public GameObject cuadroFrente;
    public GameObject cuadroAtras;

    private Camera arCamera;

    private void Start()
    {
        arCamera = Camera.main;
    }

    private void Update()
    {
        if (arCamera == null)
            return;

        Vector3 dirCamara = arCamera.transform.position - transform.position;

        float dot = Vector3.Dot(transform.forward, dirCamara);

        if (dot >= 0)
        {
            cuadroFrente.SetActive(true);
            cuadroAtras.SetActive(false);
        }
        else
        {
            cuadroFrente.SetActive(false);
            cuadroAtras.SetActive(true);
        }
    }
}