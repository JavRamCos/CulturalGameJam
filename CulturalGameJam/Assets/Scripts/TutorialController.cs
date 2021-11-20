using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TutorialController : MonoBehaviour {
    public static TutorialController instance;
    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    public void EndTutorial() {
        PlayerPrefs.SetInt("CompletedTutorial", 1);
        SceneManager.LoadScene("Level1");
    }

    public void RepeatTutorial() {
        SceneManager.LoadScene("Tutorial");
    }
}
