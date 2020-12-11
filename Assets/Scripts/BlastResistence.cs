using UnityEngine;

public class BlastResistence : MonoBehaviour
{
    [SerializeField] private DestructableItem destructableItem = null;
    [SerializeField] private AudioEvent hitAudio = null;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private ParticleSystem explodeEffects = null;
    [SerializeField] private ParticleSystem damageEffects = null;
    private float curLife;

    private SpriteRenderer _spriteRenderer = null;

	// Start is called before the first frame update
	void Start()
    {
		curLife = destructableItem.blastResistence;

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.sprite = destructableItem.fullLifeSprite;
	}

    private void VerifyDestructionAmount()
    {
        if (curLife <= 0)
        {
            if (explodeEffects != null)
            {
                explodeEffects.transform.parent = null;
                explodeEffects.Play();
                Destroy(explodeEffects.gameObject, 2f);
            }
            if (audioSource != null && hitAudio != null)
            {
                audioSource.transform.parent = null;
                Destroy(audioSource.gameObject, 1f);
            }
            Destroy(gameObject);
            return;
        }

        if (curLife > (destructableItem.blastResistence * 0.66f))
            _spriteRenderer.sprite = destructableItem.fullLifeSprite;
        else if (curLife > (destructableItem.blastResistence * 0.33f))
            _spriteRenderer.sprite = destructableItem.middleLifeSprite;
        else
            _spriteRenderer.sprite = destructableItem.lowLifeSprite;
    }

    void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Explosion")
			Degrade(1);
	}

	public void Degrade(int points)
	{
		curLife -= points;
        if (damageEffects != null)
            damageEffects.Play();
        if (audioSource != null && hitAudio != null)
            hitAudio.Play(audioSource);
        VerifyDestructionAmount();
    }
}
