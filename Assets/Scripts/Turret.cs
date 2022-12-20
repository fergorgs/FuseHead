using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Turret : NetworkBehaviour {

    public GameObject bullet = null;
    public Transform bulletPoint = null;
    [SerializeField] private AudioEvent fireAudio = default;

    private TurretRotationScript _head = null;
    private AudioSource _audioSource = null;

    public float timeToShoot = 1f;
    private NetworkVariable<float> _elapsedTime = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private void Awake()
    {
        _head = GetComponentInChildren<TurretRotationScript>();
        _audioSource = GetComponent<AudioSource>();
    }

    /*private void OnEnable()
    {
        _elapsedTime.OnValueChanged += UpdateSpriteColor;
    }

    private void OnDisable()
    {
        _elapsedTime.OnValueChanged -= UpdateSpriteColor;
    }*/

    private void Fire()
    {
        if(_head.target)
            SpawnBulletServerRpc(bulletPoint.position, bulletPoint.rotation);
        fireAudio?.Play(_audioSource);
    }

    // Update is called once per frame
    private void Update()
    {
        if(!_head.target)
        {
            _elapsedTime.Value = 0;
            return;
        }
        if(_elapsedTime.Value > timeToShoot)
        {
            Fire();
            _elapsedTime.Value = 0f;
        }
        _elapsedTime.Value += Time.deltaTime;
    }

    [ServerRpc]
    private void SpawnBulletServerRpc(Vector3 position, Quaternion rotation)
    {
        GameObject spawnedBullet = Instantiate(bullet, position, rotation);
        spawnedBullet.GetComponent<NetworkObject>().Spawn(true);
    }

}
