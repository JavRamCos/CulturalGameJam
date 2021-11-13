using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public int health;
    public int maxHealth = 5;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

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

    public void receiveHealth(int hp) {
        health += hp;
    }

    public int GetHealth() { return health; }
}
