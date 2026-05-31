using UnityEngine;
using Vuforia;

public class AudioImageTarget : MonoBehaviour
{
    private ObserverBehaviour observer;
    public AudioSource audioSource;

    void Start()
    {
        observer = GetComponent<ObserverBehaviour>();

        if (observer)
        {
            observer.OnTargetStatusChanged += OnStatusChanged;
        }
    }

    private void OnStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED ||
            status.Status == Status.EXTENDED_TRACKED)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Pause();
        }
    }

    private void OnDestroy()
    {
        if (observer)
        {
            observer.OnTargetStatusChanged -= OnStatusChanged;
        }
    }
}