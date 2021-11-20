using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    [SerializeField] public Transform destination;
    [SerializeField] public SpriteRenderer greenHalo;
    [SerializeField] public SpriteRenderer redHalo;

    private void Start()
    {
        greenHalo.enabled = false;
        redHalo.enabled = false;
    }

    public Transform GetDestination()
    {
        return destination;
    }

    public void EnaibleGreenHalo()
    {
        greenHalo.enabled = true;
    }
    public void DisableGreenHalo()
    {
        greenHalo.enabled = false;
    }
    public void EnaibleRedHalo()
    {
        redHalo.enabled = true;
    }
    public void DisableRedHalo()
    {
        redHalo.enabled = false;
    }
}
