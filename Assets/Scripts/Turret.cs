using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public GameObject bullet;
    public Transform bulletPoint;

    public float timeToShoot = 1f;
    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start() {
        
    }

    void Fire() {
        Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
    }

    // Update is called once per frame
    void Update() {
        if(elapsedTime > timeToShoot) {
            Fire();
            elapsedTime = 0f;
        }
        elapsedTime += Time.deltaTime;
    }

}
