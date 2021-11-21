using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerIntensity : MonoBehaviour
{
    public static PlayerIntensity instance;
    public Light2D playerLight;
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
        playerLight.GetComponent<Light2D>();
        playerLight.pointLightOuterRadius = 1.18f;
        playerLight.pointLightInnerRadius = 0f;
        playerLight.enabled = false;
    }
    
    public void LightIntensity()
    {
        playerLight.pointLightOuterRadius += 0.3f;
        playerLight.pointLightInnerRadius += 0.2f;
    }

}
