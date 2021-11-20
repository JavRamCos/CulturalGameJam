using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTeleporter != null)
            {
                Vector3 offset = new Vector3(0, 0.5f, 0); 
                transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position + offset; 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Telerporter"))
        {
            currentTeleporter = collision.gameObject;
            currentTeleporter.GetComponent < Teleporter>().EnaibleGreenHalo();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Telerporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter.GetComponent<Teleporter>().DisableGreenHalo();
                currentTeleporter = null;
            }
        }
    }

}
