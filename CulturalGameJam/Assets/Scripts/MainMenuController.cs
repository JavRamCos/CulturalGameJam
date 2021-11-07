using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController instance;
    void Awake() {
        if(instance == null) {
            instance = this;
        }
    }
}
