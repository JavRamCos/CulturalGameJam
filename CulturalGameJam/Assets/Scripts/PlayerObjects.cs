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
        //PlayerPrefs.SetInt("HasPelota", 0);
        //PlayerPrefs.SetInt("HasVeneno", 0);
        //PlayerPrefs.SetInt("HasPluma", 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "OneSidedSpike") {
            OneSidedSpike oss = collision.gameObject.GetComponent<OneSidedSpike>();
            if(oss != null) {
                PlayerHealth.instance.takeHit(1, collision);
            }
        } else if(collision.gameObject.tag == "AllSidedSpike") {
            AllSidedSpike sc = collision.gameObject.GetComponent<AllSidedSpike>();
            if(sc != null) {
                PlayerHealth.instance.takeHit(1, collision);
            }
        } else if(collision.gameObject.tag == "Collectible") {
            CollectibleController cc = collision.gameObject.GetComponent<CollectibleController>();
            if(cc != null) {
                cc.ActivateCollectibleDialogue();
            }
        } else if(collision.gameObject.tag == "Ammo") {
            PlayerAbilities.instance.GetAmmo(5);
            Destroy(collision.gameObject);
            SoundsController sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundsController>();
            if(sc != null) {
                sc.PlaySound(6);
            }
        } else if (collision.gameObject.tag == "Health") {
            PlayerHealth.instance.receiveHealth(1);
            Destroy(collision.gameObject);
            SoundsController sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundsController>();
            if (sc != null) {
                sc.PlaySound(6);
            }
        } else if(collision.gameObject.tag == "Veneno") {
            PlayerPrefs.SetInt("HasVeneno", 1);
            Destroy(collision.gameObject);
            MosquitoController.instance.ShowDialogue(3);
            SoundsController sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundsController>();
            if (sc != null) {
                sc.PlaySound(6);
            }
        } else if(collision.gameObject.tag == "Pluma") {
            PlayerPrefs.SetInt("HasPluma", 1);
            PlayerIntensity.instance.LightIntensity();
            Destroy(collision.gameObject);
            MosquitoController.instance.ShowDialogue(2);
            SoundsController sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundsController>();
            if (sc != null) {
                sc.PlaySound(6);
            }
        } else if (collision.gameObject.tag == "Pelota") {
            PlayerMovement.instance.AddJump();
            PlayerPrefs.SetInt("HasPelota", 1);
            Destroy(collision.gameObject);
            MosquitoController.instance.ShowDialogue(1);
            SoundsController sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundsController>();
            if (sc != null) {
                sc.PlaySound(6);
            }
        } else if (collision.gameObject.name == "TutorialEnd") {
            TutorialController tc = GameObject.FindGameObjectWithTag("GameController").GetComponent<TutorialController>();
            if(tc != null) {
                tc.EndTutorial();
            }
        } else if (collision.gameObject.tag == "BossProjectile") {
            PlayerHealth.instance.takeHit(4, collision);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "River") {
            TutorialController tc = GameObject.FindGameObjectWithTag("GameController").GetComponent<TutorialController>();
            if(tc != null) {
                tc.RepeatTutorial();
            }
        } else if (collision.gameObject.tag == "Balam") {
            PlayerPrefs.SetInt("HasBalam", 1);
            PlayerMovement.instance.AddDash();
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "OneSidedSpike")
        {
            OneSidedSpike oss = collision.gameObject.GetComponent<OneSidedSpike>();
            if (oss != null)
            {
                PlayerHealth.instance.takeHit(1, collision);
            }
        }
        else if (collision.gameObject.tag == "AllSidedSpike")
        {
            AllSidedSpike sc = collision.gameObject.GetComponent<AllSidedSpike>();
            if (sc != null)
            {
                PlayerHealth.instance.takeHit(1, collision);
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
