using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    protected Vector2 movement;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public float speed = 5f;
    [SerializeField] public float jumpf = 5f;
    private bool grounded;
    private bool hasDoubleJump = false;
    private bool canDouleJump = false;
    private bool isDead = false;
    [SerializeField] protected Animator animator;
    private bool isDashing = false;
    public float dashForce = 15f;
    private float dashStartTimer = 0.25f;
    private float dashTimer;
    private bool hasDash = false;
    private float dashCooldown = 2f;
    private bool canDash;

    //[SerializeField] private Animator animator;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false) {
            movement.x = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

            if (movement.x > 0) {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (movement.x < 0) {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            if (Input.GetButtonDown("Jump") && (grounded || canDouleJump)) {
                SoundsController sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundsController>();
                if (sc != null) {
                    sc.PlaySound(1);
                }
                rb.velocity = new Vector2(rb.velocity.x, jumpf);
                if (grounded)
                    grounded = false;
                else if (canDouleJump)
                    canDouleJump = false;
                animator.SetBool("Jumping", true);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && hasDash && movement.x != 0 && canDash)
            {
                SoundsController sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundsController>();
                if (sc != null) {
                    sc.PlaySound(3);
                }
                canDash = false;
                isDashing = true;
                dashTimer = dashStartTimer;
                rb.velocity = Vector2.zero;
            }
            if (isDashing)
            {
                rb.velocity = transform.right * dashForce;
                dashTimer -= Time.deltaTime;
                if(dashTimer <= 0)
                {
                    isDashing = false;
                }
            }
            if (!canDash)
            {
                dashCooldown -= Time.deltaTime;
                if (dashCooldown <= 0f)
                {
                    canDash = true;
                    dashCooldown = 2f;
                }
            }
        }

        

        animator.SetFloat("Speed", movement.sqrMagnitude);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "OneSidedSpike" || collision.gameObject.tag == "AllSidedSpike")
        {
            grounded = true;
            if (hasDoubleJump)
                canDouleJump = true;
            animator.SetBool("Jumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "ground") {
            grounded = false;
        }
    }

    public void AddJump()
    {
        hasDoubleJump = true;
        canDouleJump = true;

    }
    public void AddDash()
    {
        hasDash = true;

    }

    public void SetIsDead(bool value) {
        isDead = value;
    }

}
