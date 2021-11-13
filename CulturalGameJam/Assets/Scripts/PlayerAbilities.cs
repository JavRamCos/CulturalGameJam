using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public static PlayerAbilities instance;
    public GameObject bullet;
    public Transform fireDest;
    public Transform fireDestUp;
    public float shootTime;
    private float stime = 0f;
    private bool canShoot = true;
    private bool isDead = false;
    public float fireSpeed = 10f;
    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false) {
            if (Input.GetButtonDown("Fire1") && canShoot) {
                Transform direction = fireDest;
                if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") > 0)
                    direction = fireDestUp;
                GameObject proj = Instantiate(bullet, direction.position, direction.rotation) as GameObject;
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.velocity = direction.transform.right * fireSpeed;
                canShoot = false;
            }

            if (!canShoot) {
                stime += Time.deltaTime;
                if (stime >= shootTime) {
                    canShoot = true;
                    stime = 0f;
                }
            }
        }
    }

    public void SetIsDead(bool value) {
        isDead = value;
    }
}
