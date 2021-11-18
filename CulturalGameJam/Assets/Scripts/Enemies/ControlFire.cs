using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFire : MonoBehaviour
{
    public AIPatrol[] enemyArray;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (AIPatrol enemy in enemyArray)
            {
                enemy.fire = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (AIPatrol enemy in enemyArray)
            {
                enemy.fire = false;
            }
        }
    }
}
