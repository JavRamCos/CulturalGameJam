using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour {
    public static CollectibleController instance;

    [SerializeField] private Animator collectibleAnim;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    public void ActivateCollectibleDialogue() {
        collectibleAnim.SetBool("IsInside", true);
    }

    public void DeactivateCollectibleDialogue() {
        collectibleAnim.SetBool("IsInside", false);
    }
}
