using System.Collections.Generic;
using UnityEngine;

public class Teletransport : MonoBehaviour//, IInteractable
{
    [SerializeField] private Teletransport target = null;
    [SerializeField] private AudioEvent teleportAudio = null;
    private AudioSource _audioSource = null;

    private HashSet<GameObject> arrivingPlayers = new HashSet<GameObject>();

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void receivePlayer(GameObject player)
    {
        arrivingPlayers.Add(player);
        player.transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
        teleportAudio?.Play(_audioSource);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !arrivingPlayers.Contains(other.gameObject))
            target.receivePlayer(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        arrivingPlayers.Remove(other.gameObject);
    }
}
