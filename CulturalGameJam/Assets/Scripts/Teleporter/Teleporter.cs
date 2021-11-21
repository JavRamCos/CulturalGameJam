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
    public bool isTutorial;
    GameObject grand_parent;
    public CreateRoom room_creator;

    private void Start()
    {
        greenHalo.enabled = false;
        redHalo.enabled = false;
        if (!isTutorial)
        {
            room_creator = transform.parent.GetComponentInParent<CreateRoom>();
        }


    }

    public Transform GetDestination()
    {
        if (!isBoss && !isTutorial)
        {
            grand_parent = GameObject.Find("Area1 (1)");
            if (grand_parent.transform.GetChild(0).transform.GetChild(0).name == gameObject.name)
            {
                isSecondArea = true;
            }
            if (isSecondArea)
            {
                AstarPath.active.Scan();
                
                return destination;
            }

            
            room_creator.HideCurrentArea();
            return destination;
        }
        else if (isTutorial)
        {
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
