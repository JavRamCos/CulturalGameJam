using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class IntroController : MonoBehaviour {
    public static IntroController instance;

    [SerializeField] private Text textOutput;
    private string[] texts = { "\nText 1", "Text 2", "Text 3"};
    private int counter;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
        counter = 0;
        textOutput.text = texts[0];
    }

    public void ButtonFunction() {
        counter++;
        if (counter < texts.Length) {
            ShowNextText();
        } else {
            PlayGame();
        }
    }

    private void ShowNextText() {
        textOutput.text = texts[counter];
    }

    private void PlayGame() {
        if (PlayerPrefs.GetInt("CompletedTutorial") == 0) {
            SceneManager.LoadScene("Tutorial");
        } else {
            SceneManager.LoadScene("Level1");
        }
    }
}
