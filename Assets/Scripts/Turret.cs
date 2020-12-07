using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public GameObject bullet = null;
    public Transform bulletPoint = null;
    [SerializeField] private AudioEvent fireAudio = default;

    private TurretRotationScript _head = null;
    private AudioSource _audioSource = null;

    public float timeToShoot = 1f;
    private float _elapsedTime = 0f;

    private void Awake()
    {
        _head = GetComponentInChildren<TurretRotationScript>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Fire()
    {
        if(_head.target)
            Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
        fireAudio?.Play(_audioSource);
    }

    // Update is called once per frame
    private void Update()
    {
        if(!_head.target)
        {
            _elapsedTime = 0;
            return;
        }
        if(_elapsedTime > timeToShoot)
        {
            Fire();
            _elapsedTime = 0f;
        }
        _elapsedTime += Time.deltaTime;
    }

}
