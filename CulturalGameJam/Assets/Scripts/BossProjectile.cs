using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
            if(ph != null) {
                ph.takeHitCol(1, collision);
            }
        }
        Destroy(this.gameObject);
    }
}
