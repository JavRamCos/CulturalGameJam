using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class LightMap : MonoBehaviour
{
    public static LightMap instance;
    public Light2D GlobalLight;
    private float intensity;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GlobalLight.GetComponent<Light2D>();
        GlobalLight.intensity = 1;
        intensity = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerIntensity.instance.playerLight.enabled = true;
            GlobalLight.intensity = intensity;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerIntensity.instance.playerLight.enabled = false;
            GlobalLight.intensity = 1;
        }
    }
    public void LightIntensity()
    {
        GlobalLight.intensity += 0.016f;
    }
}
