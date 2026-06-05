using System.Collections;
using UnityEngine;

public class ControlUI : MonoBehaviour
{
    public GameObject[] uiElements;
    public float showTime = 2f;

    private Coroutine sequenceCoroutine;
    private bool targetFound = false;

    private void Start()
    {
        HideAll();
    }

    public void OnTargetFound()
    {
        if (targetFound) return;

        targetFound = true;

        if (sequenceCoroutine != null)
            StopCoroutine(sequenceCoroutine);

        sequenceCoroutine = StartCoroutine(PlaySequence());
    }

    public void OnTargetLost()
    {
        targetFound = false;

        if (sequenceCoroutine != null)
        {
            StopCoroutine(sequenceCoroutine);
            sequenceCoroutine = null;
        }

        HideAll();
    }

    private IEnumerator PlaySequence()
    {
        int index = 0;

        while (targetFound)
        {
            HideAll();

            uiElements[index].SetActive(true);

            yield return new WaitForSeconds(showTime);

            index++;

            if (index >= uiElements.Length)
                index = 0;
        }
    }

    private void HideAll()
    {
        foreach (GameObject obj in uiElements)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }
}