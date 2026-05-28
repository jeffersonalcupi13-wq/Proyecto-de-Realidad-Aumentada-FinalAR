using UnityEngine;

public class Qrcontrol : MonoBehaviour
{
    public GameObject advertencia;

    // Esta función se ejecuta cuando se detecta el QR
    public void QRDetectado()
    {
        advertencia.SetActive(false);
        Debug.Log("Advertencia quitada");
    }
}