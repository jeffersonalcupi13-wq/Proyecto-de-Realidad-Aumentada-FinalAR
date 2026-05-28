using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenamanager : MonoBehaviour
{
    public void IrAMenu()
    {
        SceneManager.LoadScene("000 - Menu");
    }

    public void IrModel()
    {
        SceneManager.LoadScene("01 - ModelTarget");
    }

    public void IrImage()
    {
        SceneManager.LoadScene("02 - ImageTarget");
    }

    public void IrBarcode()
    {
        SceneManager.LoadScene("03 - Barcode");
    }
}
