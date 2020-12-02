using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public GameObject bullet;
    public Transform bulletPoint;

    private TurretRotationScript head = null;

    public float timeToShoot = 1f;
    private float elapsedTime = 0f;

    void Awake() {
        head = GetComponentInChildren<TurretRotationScript>();
    }

    void Fire() {
        if(head.target)
            Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
    }

    // Update is called once per frame
    void Update() {
        if(!head.target) {
            elapsedTime = 0;
            return;
        }
        if(elapsedTime > timeToShoot) {
            Fire();
            elapsedTime = 0f;
        }
        elapsedTime += Time.deltaTime;
    }

}
