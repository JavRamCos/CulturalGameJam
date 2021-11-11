using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 5;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    //Recibir danio
    public void takeHit(int hp)
    {
        health -= hp;
        //if(health <= 0) se murio
    }
}
