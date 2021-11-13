using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player") {
            PlayerHealth ph = other.gameObject.GetComponent<PlayerHealth>();
            if(ph != null) {
                ph.takeHit(1);
            }
        }
        Destroy(this.gameObject);
    }
}
