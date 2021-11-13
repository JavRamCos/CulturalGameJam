using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjects : MonoBehaviour
{
    [SerializeField]
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "OneSidedSpike") {
            OneSidedSpike oss = collision.gameObject.GetComponent<OneSidedSpike>();
            if(oss != null) {
                PlayerHealth.instance.takeHit(1);
            }
        } else if(collision.gameObject.tag == "AllSidedSpike") {
            AllSidedSpike sc = collision.gameObject.GetComponent<AllSidedSpike>();
            if(sc != null) {
                PlayerHealth.instance.takeHit(1);
            }
        } else if(collision.gameObject.tag == "Collectible") {
            CollectibleController cc = collision.gameObject.GetComponent<CollectibleController>();
            if(cc != null) {
                cc.ActivateCollectibleDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Collectible") {
            CollectibleController cc = collision.gameObject.GetComponent<CollectibleController>();
            if (cc != null) {
                cc.DeactivateCollectibleDialogue();
            }
        }
    }
}
