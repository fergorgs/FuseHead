using System;
using UnityEngine;

public class FlagObject : MonoBehaviour
{
    public Action OnFlagCaptured = null;

    [SerializeField] private AudioEvent flagCapturedSFX = null;
    [SerializeField] private ParticleSystem capturedEffects = null;
    [SerializeField] private AudioSource audioSource = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        FlagCaptured();
        
    }

    private void FlagCaptured()
    {
        if (audioSource != null)
        {
            audioSource.transform.parent = null;
            flagCapturedSFX.Play(audioSource);
            Destroy(audioSource.gameObject, 2f);
        }
        if(capturedEffects != null)
        {
            capturedEffects.transform.parent = null;
            capturedEffects.Play();
            Destroy(capturedEffects.gameObject, 2f);
        }

        OnFlagCaptured?.Invoke();
        Destroy(gameObject);
    }
}
