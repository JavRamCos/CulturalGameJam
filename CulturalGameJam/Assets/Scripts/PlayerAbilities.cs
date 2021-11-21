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
    [SerializeField] protected Animator animator;
    public int ammo = 5;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false) {
            if (Input.GetButtonDown("Fire1") && canShoot) {
                if (ammo > 0)
                {
                    Transform direction = fireDest;
                    if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") > 0)
                    {
                        direction = fireDestUp;
                        animator.SetBool("ShootUp", true);
                        Invoke("ChangeUpBool", 0.4f);
                    }
                    else
                    {
                        animator.SetBool("Shooting", true);
                        Invoke("ChangeBool", 0.4f);
                    }
                    GameObject proj = Instantiate(bullet, direction.position, direction.rotation) as GameObject;
                    Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                    rb.velocity = direction.transform.right * fireSpeed;
                    canShoot = false;
                    ammo -= 1;
                    SoundsController sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundsController>();
                    if(sc != null) {
                        sc.PlaySound(2);
                    }
                }
                else
                {
                    animator.SetBool("Shooting", true);
                    Invoke("ChangeBool", 0.4f);
                }
                

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

    private void ChangeBool()
    {
        animator.SetBool("Shooting", false);

    }

    private void ChangeUpBool()
    {
        animator.SetBool("ShootUp", false);

    }

    public void SetIsDead(bool value) {
        isDead = value;
    }
    public void GetAmmo(int amount)
    {
        ammo += amount;
    }
}
