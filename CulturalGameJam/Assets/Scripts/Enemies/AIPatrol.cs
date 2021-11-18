using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public float speed, timeBTWShots, shootSpeed, radius;
    [HideInInspector]

    public bool mustPatrol;
    public bool mustTurn;

    public bool fire = false;
    public bool canShoot = true;

    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Transform player, shootPos, shootPos2, limitRange;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        radius = 5f;
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
        if (fire)
        {
            if (player.position.x > transform.position.x && transform.localScale.x < 0
                || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }
            mustPatrol = false;
            rb.velocity = Vector2.zero;
            Transform direction = shootPos;
            if ((player.position.y > limitRange.position.y))
            {
                if (canShoot)
                {
                    direction = shootPos2;
                    StartCoroutine(Shoot(45f, 90f, 5, direction));
                }
            }
            else if (player.position.x > transform.position.x)
            {
                if (canShoot)
                {
                    StartCoroutine(Shoot(45f, 90f, 3, direction));
                }
            }else if (player.position.x < transform.position.x) {
                if (canShoot)
                {
                    StartCoroutine(Shoot(45f, 0f, 3, direction));
                }
            }
        }
        else
        {
            mustPatrol = true;
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

    IEnumerator Shoot(float angleStep, float angle, int numberOfProjectiles, Transform direction)
    {
        canShoot = false;
        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {
            float projectileDirXposition = direction.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = direction.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - (Vector2)direction.position).normalized * shootSpeed;

            var proj = Instantiate(bullet, direction.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle -= angleStep;

        }
        yield return new WaitForSeconds(timeBTWShots);
        canShoot = true;
    }
}