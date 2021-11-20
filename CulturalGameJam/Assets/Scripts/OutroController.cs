using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OutroController : MonoBehaviour
{
    public void PlayAgain() {
        SceneManager.LoadScene("Level1");
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
