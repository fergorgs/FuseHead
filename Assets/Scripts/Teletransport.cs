using UnityEngine;

public class Teletransport : MonoBehaviour, IInteractable
{
    [SerializeField] private Teletransport target = null;
    [SerializeField] private AudioEvent teleportAudio = null;
    private AudioSource _audioSource = null;

    [HideInInspector] public bool isTarget = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTarget)
        {
            target.isTarget = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(isTarget) isTarget = false;
    }

    public void Interact(GameObject player)
    {
        player.transform.position = new Vector2(target.transform.position.x, target.transform.position.y + 1f);
        teleportAudio?.Play(_audioSource);
    }
}
