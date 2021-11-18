using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneController : MonoBehaviour
{
    private void Awake() {
        PlayerPrefs.SetInt("HasVeneno", 0);
        PlayerPrefs.SetInt("HasPluma", 0);
        PlayerPrefs.SetInt("HasPelota", 0);
    }
}
