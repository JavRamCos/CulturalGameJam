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
    private bool isDead = false;
    [SerializeField] protected Animator animator;

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
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            if (movement.x < 0) {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (Input.GetButtonDown("Jump") && grounded) {
                rb.velocity = new Vector2(rb.velocity.x, jumpf);
                grounded = false;
                animator.SetBool("Jumping", true);
            }
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
            animator.SetBool("Jumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "ground") {
            grounded = false;
        }
    }

    public void SetIsDead(bool value) {
        isDead = value;
    }

}
