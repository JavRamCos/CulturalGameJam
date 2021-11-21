using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public static AIPatrol instance;
    public float speed, timeBTWShots, shootSpeed, radius, frozenTime;
    [HideInInspector]

    public bool mustPatrol;
    public bool mustTurn;

    public int health;
    public int maxHealth;

    public bool fire = false;
    public bool canShoot = true;

    public bool frozen;

    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Transform shootPos, shootPos2, limitRange;
    public GameObject bullet;
    private GameObject player;

    [SerializeField] protected Animator anim;
    private bool attackMode;


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
        radius = 5f;
        mustPatrol = true;
        frozen = false;
        attackMode = true;
        blink = GetComponent<Blink>();
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
            if (fire)
            {
                if (player.transform.position.x > transform.position.x && transform.localScale.x < 0
                    || player.transform.position.x < transform.position.x && transform.localScale.x > 0)
                {
                    Flip();
                }
                mustPatrol = false;
                rb.velocity = Vector2.zero;
                Transform direction = shootPos;
                if ((player.transform.position.y > limitRange.position.y))
                {
                    if (canShoot)
                    {
                        direction = shootPos2;
                        StartCoroutine(Shoot(45f, 90f, 5, -90f, direction));
                    }
                }
                else if (player.transform.position.x > transform.position.x)
                {
                    if (canShoot)
                    {
                        StartCoroutine(Shoot(45f, 90f, 3, -90f, direction));
                    }
                }else if (player.transform.position.x < transform.position.x) {
                    if (canShoot)
                    {
                        StartCoroutine(Shoot(45f, 0f, 3, 0f, direction));
                    }
                }
                anim.SetBool("Ataque", true);
            }
            else
            {
                mustPatrol = true;
                anim.SetBool("Ataque", false);
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

    IEnumerator Shoot(float angleStep, float angle, int numberOfProjectiles, float angleRotation, Transform direction)
    {
        canShoot = false;
        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {
            float projectileDirXposition = direction.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = direction.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - (Vector2)direction.position).normalized * shootSpeed;

            var proj = Instantiate(bullet, direction.position, Quaternion.Euler(new Vector3(0f, 0f, angleRotation)));
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle -= angleStep;
            angleRotation += angleStep;

        }
        yield return new WaitForSeconds(timeBTWShots);
        canShoot = true;
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
                StartCoroutine(blinkEffect());
                StartCoroutine(Frozen());
            }
        }
    }
}
