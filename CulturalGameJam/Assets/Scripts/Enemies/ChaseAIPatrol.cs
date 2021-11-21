using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAIPatrol : MonoBehaviour
{
    public static ChaseAIPatrol instance;
    public float speed, frozenTime, timeBTWShots, shootSpeeed;
    [HideInInspector]

    public bool mustPatrol;
    public bool mustTurn;

    public int health;
    public int maxHealth;

    public bool chase = false;
    public bool frozen;

    private bool canShoot = true;

    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public LayerMask enemy;
    public Collider2D bodyCollider;
    public Transform shootPos;
    public GameObject bullet;
    private GameObject player;

    [SerializeField] public SpriteRenderer sprite;
    Blink blink;

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
        blink = GetComponent<Blink>();
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
                if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer) || bodyCollider.IsTouchingLayers(enemy))
                {
                    mustPatrol = false;
                    rb.velocity = Vector2.zero;
                    if (player.transform.position.x > transform.position.x && transform.localScale.x < 0
                        || player.transform.position.x < transform.position.x && transform.localScale.x > 0)
                    {
                        Flip();
                        mustPatrol = true;
                    }
                }
                Debug.Log((Mathf.Abs(player.transform.position.x - transform.position.x)));
                if (Mathf.Abs(player.transform.position.x - transform.position.x) >= 7f)
                {
                    if (canShoot)
                    {
                        StartCoroutine(shoot());
                    }
                }
            }
            else
            {
                mustPatrol = true;
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
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer) || bodyCollider.IsTouchingLayers(enemy))
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

    IEnumerator blinkEffect()
    {
        sprite.material = blink.blink;
        yield return new WaitForSeconds(0.5f);
        sprite.material = blink.original;
    }

    IEnumerator shoot()
    {
        canShoot = false;
        float direccion = transform.localScale.x;
        Debug.Log(direccion);
        if (direccion == 1f)
        {
            GameObject newBuller = Instantiate(bullet, shootPos.position, Quaternion.Euler(new Vector3(0f, 0f, -90f)));
            newBuller.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeeed * speed * Time.fixedDeltaTime, 0f);
        }
        else
        {
            GameObject newBuller = Instantiate(bullet, shootPos.position, Quaternion.Euler(new Vector3(0f, 0f, 90f)));
            newBuller.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeeed * speed * Time.fixedDeltaTime, 0f);
        }
        yield return new WaitForSeconds(timeBTWShots);
        canShoot = true;
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
                StartCoroutine(blinkEffect());
            }
        }
    }
}
