using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameController : MonoBehaviour
{
    public Light2D GlobalLight;
    PlayerIntensity playerLight;

    // Start is called before the first frame update
    void Start()
    {
        GlobalLight.GetComponent<Light2D>();
        GlobalLight.intensity = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerLight.enabled = true;
            GlobalLight.intensity = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerLight.enabled = false;
            GlobalLight.intensity = 1;
        }
    }
}

