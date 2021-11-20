using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public int health;
    public int maxHealth = 10;
    [SerializeField] protected Animator animator;
    private bool canGetDamage;
    public float knockbackForceX;
    public float knockbackForceY;
    public float knockbackForceXUp;
    public float knockbackForceYUp;
    Rigidbody2D rb;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        canGetDamage = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    //Recibir danio
    public void takeHit(int hp, Collider2D collision)
    {
        if (canGetDamage == true) {
            health -= hp;
            SoundsController sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundsController>();
            if (sc != null) {
                sc.PlaySound(4);
            }
            canGetDamage = false;
            if (health <= 0) {
                animator.SetBool("Dead", true);
                GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
                PauseController pc = gameManager.GetComponent<PauseController>();
                if (pc != null) {
                    PlayerMovement.instance.SetIsDead(true);
                    PlayerAbilities.instance.SetIsDead(true);
                    pc.ShowLosePanel();
                }
            } else {
                animator.SetBool("PlayerHit", true);
                knockback(collision);
                Invoke("HitAnimation", 0.5f);
            }
            Invoke("SetCanGetDamage", 2.0f);
        }
    }

    public void takeHitCol(int hp, Collision2D collision)
    {
        if (canGetDamage == true)
        {
            health -= hp;
            SoundsController sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundsController>();
            if (sc != null)
            {
                sc.PlaySound(4);
            }
            canGetDamage = false;
            if (health <= 0)
            {
                animator.SetBool("Dead", true);
                GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
                PauseController pc = gameManager.GetComponent<PauseController>();
                if (pc != null)
                {
                    PlayerMovement.instance.SetIsDead(true);
                    PlayerAbilities.instance.SetIsDead(true);
                    pc.ShowLosePanel();
                }
            }
            else
            {
                animator.SetBool("PlayerHit", true);
                knockbackCol(collision);
                Invoke("HitAnimation", 0.5f);
            }
            Invoke("SetCanGetDamage", 2.0f);
        }
    }

    public void SetCanGetDamage() {
        canGetDamage = true;
    }

    private void HitAnimation()
    {
        animator.SetBool("PlayerHit", false);

    }
    public void receiveHealth(int hp) {
        health += hp;
    }

    public int GetHealth() { return health; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            takeHit(1, collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            rb.velocity = Vector2.zero;
            takeHit(1, collision);
        }
    }

    public void knockback(Collider2D collision)
    {
        if (collision.transform.position.y < transform.position.y)
        {
            if (rb.velocity.y > 0) {
                if (collision.transform.position.x > transform.position.x)
                {
                    rb.AddForce(new Vector2(-knockbackForceXUp * 2.5f, 0f), ForceMode2D.Force);
                }
                else if (collision.transform.position.x > transform.position.x)
                {
                    rb.AddForce(new Vector2(knockbackForceXUp * 2.5f, 0f), ForceMode2D.Force);
                }
            }
            else
            {
                if (collision.transform.position.x > transform.position.x)
                {
                    rb.AddForce(new Vector2(-knockbackForceXUp, knockbackForceYUp), ForceMode2D.Force);
                }
                else if (collision.transform.position.x > transform.position.x)
                {
                    rb.AddForce(new Vector2(knockbackForceXUp, knockbackForceYUp), ForceMode2D.Force);
                }
                else
                {
                    rb.AddForce(new Vector2(0f, knockbackForceYUp), ForceMode2D.Force);
                }
            }
        }
        else if (collision.transform.position.x > transform.position.x)
        {
            rb.AddForce(new Vector2(-knockbackForceX, knockbackForceY), ForceMode2D.Force);
        }
        else if (collision.transform.position.x < transform.position.x)
        {
            rb.AddForce(new Vector2(knockbackForceX, knockbackForceY), ForceMode2D.Force);
        }
    }
    public void knockbackCol(Collision2D collision)
    {
        if (collision.transform.position.y < transform.position.y)
        {
            if (rb.velocity.y > 0)
            {
                if (collision.transform.position.x > transform.position.x)
                {
                    rb.AddForce(new Vector2(-knockbackForceXUp * 2.5f, 0f), ForceMode2D.Force);
                }
                else if (collision.transform.position.x > transform.position.x)
                {
                    rb.AddForce(new Vector2(knockbackForceXUp * 2.5f, 0f), ForceMode2D.Force);
                }
            }
            else
            {
                if (collision.transform.position.x > transform.position.x)
                {
                    rb.AddForce(new Vector2(-knockbackForceXUp, knockbackForceYUp), ForceMode2D.Force);
                }
                else if (collision.transform.position.x > transform.position.x)
                {
                    rb.AddForce(new Vector2(knockbackForceXUp, knockbackForceYUp), ForceMode2D.Force);
                }
                else
                {
                    rb.AddForce(new Vector2(0f, knockbackForceYUp), ForceMode2D.Force);
                }
            }
        }
        else if (collision.transform.position.x > transform.position.x)
        {
            rb.AddForce(new Vector2(-knockbackForceX, knockbackForceY), ForceMode2D.Force);
        }
        else if (collision.transform.position.x < transform.position.x)
        {
            rb.AddForce(new Vector2(knockbackForceX, knockbackForceY), ForceMode2D.Force);
        }
    }
}
