using System.Collections;
using UnityEngine;

public class HighspeedCollisionSFx : MonoBehaviour
{
    public AudioEvent audioEvent = default;
    [SerializeField] private float minSpeed = 0.5f;

    private AudioSource _audioSource = default;
    private Rigidbody2D _rigidbody2D = default;
    private const float playDelayTime = 1f;


    [SerializeField] private bool minSpeedHit = false;
    [SerializeField] private bool inPlayTime = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(PlayTimeSet());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (minSpeedHit && inPlayTime)
        {
            audioEvent?.Play(_audioSource);
            inPlayTime = false;
            StartCoroutine(PlayTimeSet());
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(_rigidbody2D.velocity.y) >= minSpeed)
        {
            StopCoroutine(MinSpeedNotHit());
            minSpeedHit = true;
        }
        else
        {
            StartCoroutine(MinSpeedNotHit());
        }
    }

    private IEnumerator MinSpeedNotHit()
    {
        yield return new WaitForEndOfFrame();
        minSpeedHit = false;
    }

    private IEnumerator PlayTimeSet()
    {
        yield return new WaitForSeconds(playDelayTime);
        inPlayTime = true;
    }
}
