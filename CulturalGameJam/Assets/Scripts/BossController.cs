using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BossController : MonoBehaviour {
    public static BossController instance;

    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;
    [SerializeField] private float positionRate;
    [SerializeField] private float ammoSpawnRate;
    [SerializeField] private Transform[] positions;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Animator BossAnim;
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private Transform spawnOne;
    [SerializeField] private Transform spawnTwo;
    private Vector3 nextPos;
    private Vector3 playerPos;
    private float counter1;
    private float counter2;
    private float counter3;
    private bool isDead;
    private bool hasNextPos;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        isDead = false;
        hasNextPos = false;
        gameObject.transform.position = positions[0].transform.position;
        counter1 = 0.0f;
        counter2 = 0.0f;
        counter3 = 0.0f;
        BossAnim.SetBool("IsDead", false);
    }

    private void FixedUpdate() {
        if (isDead == false) {
            if (hasNextPos == false) {
                nextPos = positions[Random.Range(0, positions.Length)].transform.position;
                hasNextPos = true;
            }
            ChangePosition();
            FireAtPlayer();
            SpawnAmmo();
        }
    }

    private void ChangePosition() {
        counter1 += Time.deltaTime;
        if (counter1 >= positionRate) {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nextPos, speed * Time.deltaTime);
            if (gameObject.transform.position == nextPos) {
                hasNextPos = false;
                counter1 = 0.0f;
            }
        }
    }

    private void FireAtPlayer() {
        counter2 += Time.deltaTime;
        if (counter2 >= fireRate) {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            GameObject proj = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(playerPos.x - gameObject.transform.position.x, playerPos.y - gameObject.transform.position.y) * 1.2f;
            counter2 = 0.0f;
        }
    }

    public void TakeDamage(int damage) {
        if (PlayerPrefs.GetInt("HasVeneno") == 1) {
            health -= damage;
        } else {
            health -= (int)(damage / 2);
        }
        if (health <= 0) {
            BossAnim.SetBool("IsDead", true);
            isDead = true;
            Invoke("WinGame", 1.2f);
        }
    }

    public void SpawnAmmo() {
        counter3 += Time.deltaTime;
        if(counter3 >= ammoSpawnRate) {
            int rand = Random.Range(1, 7);
            if(rand <= 3.0f) {
                Instantiate(ammoPrefab, spawnOne.position, Quaternion.identity);
            } else {
                Instantiate(ammoPrefab, spawnTwo.position, Quaternion.identity);
            }
            counter3 = 0.0f;
        }
    }

    public void WinGame() {
        SceneManager.LoadScene("Outro");
    }

}
