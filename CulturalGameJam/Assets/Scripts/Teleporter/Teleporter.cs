using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField] public Transform destination;
    [SerializeField] public SpriteRenderer greenHalo;
    [SerializeField] public SpriteRenderer redHalo;
    public bool isBoss;
    public bool isSecondArea;
    GameObject grand_parent;

    private void Start()
    {
        greenHalo.enabled = false;
        redHalo.enabled = false;
        
    }

    public Transform GetDestination()
    {
        if (!isBoss)
        {
            grand_parent = GameObject.Find("Area1 (1)");
            if (grand_parent.transform.GetChild(0).transform.GetChild(0).name == gameObject.name)
            {
                isSecondArea = true;
            }
            if (isSecondArea)
            {
                AstarPath.active.Scan();
            }
            return destination;
        }
        else
        {
            SceneManager.LoadScene("Boss");
            return destination;
        }
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
