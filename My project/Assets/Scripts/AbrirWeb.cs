using UnityEngine;

public class AbrirWeb : MonoBehaviour
{
    public string url = "https://www.google.com";

    public void AbrirPagina()
    {
        Application.OpenURL(url);
    }
}