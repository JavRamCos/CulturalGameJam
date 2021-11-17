using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MosquitoController : MonoBehaviour
{
    public static MosquitoController instance;

    [SerializeField] private GameObject canvas;
    [SerializeField] private Animator dialogueAnim;
    [SerializeField] private Text dialogueText;
    [SerializeField] private float timeForText;

    public void Awake() {
        if(instance == null) {
            instance = this;
        }
        dialogueAnim.SetBool("IsActive", false);
    }

    private void Update() {
        canvas.transform.rotation = Quaternion.identity;
    }

    private void ShowDialogue(int numDialogue) {
        switch(numDialogue) {
            case 1:
                dialogueText.text = "If you wish to follow the same path as your brothers, you should be careful. You should search for something to light your path.";
                break;
            case 2:
                dialogueText.text = "One of your brothers has died in the house of the bat.";
                break;
            case 3:
                dialogueText.text = "One of your brothers has died in the house of the bat.";
                break;
            default:
                dialogueText.text = "";
                break;
        }
        dialogueAnim.SetBool("IsActive", true);
        Invoke("DeactivateDialogue", timeForText);
    }

    private void DeactivateDialogue() {
        dialogueAnim.SetBool("IsActive", false);
    }
}
