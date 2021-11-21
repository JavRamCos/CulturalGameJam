using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public static EnemyAI instance;
    private GameObject target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float frozenTime;
    public bool frozen;

    public int health;
    public int maxHealth;

    public bool chase = false;
    public Transform starttingPoint;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndofPath = false;

    public Transform enemyGFX;

    Seeker seeker;
    Rigidbody2D rb;
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
        frozen = false;
        target = GameObject.FindGameObjectWithTag("Player");
        seeker = GetComponent<Seeker>();
        blink = GetComponent<Blink>();
        starttingPoint = transform.parent.transform; 
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (chase == true)
        {
            if (seeker.IsDone())
                seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
        }
        else
        {
            if (seeker.IsDone())
                seeker.StartPath(rb.position, starttingPoint.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!frozen)
        {
            if (path == null)
                return;
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndofPath = true;
                return;
            }
            else
            {
                reachedEndofPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (force.x >= 0.01f)
            {
                enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb.velocity.x <= -0.01f)
            {
                enemyGFX.localScale = new Vector3(1f, 1f, 1f);
            }
        }
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
