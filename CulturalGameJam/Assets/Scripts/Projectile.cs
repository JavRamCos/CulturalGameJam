using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth ph = other.gameObject.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.takeHit(1);
            }
        }
        else if (other.gameObject.tag == "Boss")
        {
            BossController bc = other.gameObject.GetComponent<BossController>();
            if (bc != null)
            {
                bc.TakeDamage(2);
            }
        }
        else if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.name.Contains("ChaseEnemyPatrol"))
            {
                ChaseAIPatrol enemy = other.gameObject.GetComponent<ChaseAIPatrol>();
                if (enemy != null)
                {
                    enemy.takeHit(2);
                }
            }
            else if (other.gameObject.name.Contains("EnemyPatrol")
                || other.gameObject.name.Contains("ShooterEnemyPatrol"))
            {
                AIPatrol enemy = other.gameObject.GetComponent<AIPatrol>();
                if (enemy != null)
                {
                    enemy.takeHit(2);
                }
            }
            else if (other.gameObject.name.Contains("Flying Enemy"))
            {
                EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    enemy.takeHit(2);
                }
            }
        }
        Destroy(this.gameObject);
    }
}
