using System;
using UnityEngine;

public class FlagObject : MonoBehaviour
{
    public Action OnFlagCaptured = null;

	void OnCollisionEnter2D(Collision2D collision)
	{
        if (!collision.gameObject.CompareTag("Player"))
            return;

        OnFlagCaptured?.Invoke();
		Destroy(gameObject);
	}
}
