using UnityEngine;
using System.Collections;

public class DetectorImagenQR : MonoBehaviour
{
    public Texture2D qrReferencia;

    public AudioSource audioSource;
    public AudioClip pitido;      // Beep corto
    public AudioClip audioFinal;  // Audio que pondrás

    public bool Comparar(Texture2D capturada)
    {
        Color[] refPix = qrReferencia.GetPixels();
        Color[] camPix = capturada.GetPixels();

        if (refPix.Length != camPix.Length)
            return false;

        int iguales = 0;

        for (int i = 0; i < refPix.Length; i++)
        {
            if (Vector4.Distance(refPix[i], camPix[i]) < 0.2f)
                iguales++;
        }

        float similitud = (float)iguales / refPix.Length;

        if (similitud > 0.90f)
        {
            StartCoroutine(ReproducirAudios());
            return true;
        }

        return false;
    }

    IEnumerator ReproducirAudios()
    {
        // 1. Pitido
        audioSource.PlayOneShot(pitido);

        // Esperar a que termine
        yield return new WaitForSeconds(pitido.length);

        // 2. Audio principal
        audioSource.PlayOneShot(audioFinal);
    }
}