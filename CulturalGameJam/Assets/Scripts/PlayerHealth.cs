using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public int health;
    public int maxHealth = 10;
    [SerializeField] protected Animator animator;


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
        

        if (health <= 0) {
            animator.SetBool("Dead", true);
            GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
            PauseController pc = gameManager.GetComponent<PauseController>();
            if(pc != null) {
                PlayerMovement.instance.SetIsDead(true);
                PlayerAbilities.instance.SetIsDead(true);
                pc.ShowLosePanel();
            }
        }
        else
        {
            animator.SetBool("PlayerHit", true);
            Invoke("HitAnimation", 0.5f);
        }
        
    }

    private void HitAnimation()
    {
        animator.SetBool("PlayerHit", false);

    }
    public void receiveHealth(int hp) {
        health += hp;
    }

    public int GetHealth() { return health; }
}
