using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectiles : MonoBehaviour
{
        [SerializeField]
        int numberOfProjectiles;

        [SerializeField]
        GameObject projectile;

        Vector2 startPoint;

        float radius, moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        radius = 5f;
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            startPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            SpawnProjectiles();
        }
    }

    void SpawnProjectiles()
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for(int i = 0; i <= numberOfProjectiles - 1; i++)
        {
            float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

            var proj = Instantiate(projectile, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            Debug.Log(projectileMoveDirection);

            angle += angleStep;

        }
    }
}

//GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity) as GameObject;
//newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * speed * Time.fixedDeltaTime, 0f);
