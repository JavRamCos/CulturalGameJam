using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAIPatrol : MonoBehaviour
{
    public static ChaseAIPatrol instance;
    public float speed, frozenTime;
    [HideInInspector]

    public bool mustPatrol;
    public bool mustTurn;

    public int health;
    public int maxHealth;

    public bool chase = false;
    public bool frozen;

    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    private GameObject player;

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
        player = GameObject.FindGameObjectWithTag("Player");
        frozen = false;
        mustPatrol = true;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!frozen)
        {
            if (mustPatrol)
            {
                Patrol();
            }
            if (chase)
            {
                if (player.transform.position.x > transform.position.x && transform.localScale.x < 0
                    || player.transform.position.x < transform.position.x && transform.localScale.x > 0)
                {
                    Flip();
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }
    }
    void Patrol()
    {
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
        mustPatrol = true;
    }
    IEnumerator Frozen()
    {
        frozen = true;
        yield return new WaitForSeconds(frozenTime);
        frozen = false;
    }

    public void takeHit(int damage)
    {
        if (PlayerPrefs.GetInt("HasVeneno") == 1)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (!frozen)
            {
                StartCoroutine(Frozen());
            }
        }
    }
}
