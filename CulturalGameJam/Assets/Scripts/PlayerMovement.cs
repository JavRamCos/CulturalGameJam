using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float movementX;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public float speed = 5f;
    [SerializeField] public float jumpf = 5f;
    private bool grounded;
    //[SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movementX * speed, rb.velocity.y);

        //Debug.Log(movementX);
        if(movementX > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if(movementX < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpf);
            grounded = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
            grounded = true;
    }

}
