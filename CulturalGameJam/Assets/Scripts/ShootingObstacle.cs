using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingObstacle : MonoBehaviour
{
    [SerializeField] private GameObject shootingPosition;
    [SerializeField] private GameObject centerPosition;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float shootingRate;
    [SerializeField] private float projectileVelocity;
    private float time = 0.0f;

    private void FixedUpdate() {
        time += Time.deltaTime;
        if(time >= shootingRate) {
            ShootProjectile();
            time = 0f;
        }
    }

    public void ShootProjectile() {
        float distanceX = shootingPosition.transform.position.x - centerPosition.transform.position.x;
        float distanceY = shootingPosition.transform.position.y - centerPosition.transform.position.y;
        GameObject proj = Instantiate(projectilePrefab, shootingPosition.transform) as GameObject;
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.velocity = shootingPosition.transform.right *projectileVelocity;
    }
}
