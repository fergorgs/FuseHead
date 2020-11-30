using System;
using UnityEngine;

public class FlagObject : MonoBehaviour
{
    public Action OnFlagCaptured = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        OnFlagCaptured?.Invoke();
        Destroy(gameObject);
    }
}
