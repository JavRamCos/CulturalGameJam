using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjects : MonoBehaviour
{
    public static PlayerObjects instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        PlayerPrefs.SetInt("HasPelota", 0);
        PlayerPrefs.SetInt("HasVeneno", 0);
        PlayerPrefs.SetInt("HasPluma", 0);
    }

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
        } else if(collision.gameObject.tag == "Ammo") {
            //Playerbilities.instance.AddAmmo();
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "Health") {
            PlayerHealth.instance.receiveHealth(1);
            Destroy(collision.gameObject);
        } else if(collision.gameObject.tag == "Veneno") {
            PlayerPrefs.SetInt("HasVeneno", 1);
            //Destroy(collision.gameObject);
        } else if(collision.gameObject.tag == "Pluma") {
            PlayerPrefs.SetInt("HasPluma", 1);
            //Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "Pelota") {
            PlayerPrefs.SetInt("HasPelota", 1);
            //Destroy(collision.gameObject);
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
