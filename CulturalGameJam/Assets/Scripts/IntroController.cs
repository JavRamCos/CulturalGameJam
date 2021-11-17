using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class IntroController : MonoBehaviour {
    public static IntroController instance;

    [SerializeField] private Text textOutput;
    [SerializeField] private Animator textOutputAnim;
    private string[] texts = { "\nThe tale of the twin brothers, Hunahpu and Ixblanque, is a well-known myth of the Popol Vuh. But what happened while his brothers were on their great journey? " +
                                "\n\nThis is the story of Ux-lanque the unknown third brother and his own journey after learning about what his brothers were doing.",
                                "His travel starts one day when he returns to his home and found his grandmother crying, he gets closer to her and asks her: Grandmother why are you crying and where are my brothers, on my way home I went to the ball game field and didn’t saw them." +
                                "\n\nHis grandmother in tears said: Ux-lanque, my dear Ux-lanque, your bothers have started a really dangerous journey, the lords of Xibalba have decided to invite them to play ball with them.",
                                "\nAfter hearing this terrible news, Ux-lanque grabbed his blowgun and started running to the entry of Xibalba, while the screams of his grandmother sounded on the back, and the longer he ran, the further her scream and cry sounded, until it went completely quiet and only the sound of the forest accompanied him.",
                                "\nHe traveled between 3 areas: First, the jungle before the entry of Xibalba. In this area he started hearing by the animals how his brothers entered Xibalba and faced a lot of challenges, after learning that his brothers went through the same road he has kept going.",
                                "\nAfter passing through the forest he entered the first challenges of Xibalba, the house of darkness. Before entering this house, he meets a tiny mosquito, this mosquito followed Hunahpu and Ixblanque, his brothers, and tells him what his brothers went thru." };
    private int counter;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
        counter = 0;
        textOutput.text = texts[0];
        textOutputAnim.SetBool("IsRead", true);
    }

    public void ButtonFunction() {
        counter++;
        if (counter < texts.Length) {
            textOutputAnim.SetBool("IsRead", false);
            Invoke("ShowNextText",0.5f);
        } else {
            PlayGame();
        }
    }

    private void ShowNextText() {
        textOutput.text = texts[counter];
        textOutputAnim.SetBool("IsRead", true);
    }

    private void PlayGame() {
        if (PlayerPrefs.GetInt("CompletedTutorial") == 0) {
            SceneManager.LoadScene("Tutorial");
        } else {
            SceneManager.LoadScene("Level1");
        }
    }
}
