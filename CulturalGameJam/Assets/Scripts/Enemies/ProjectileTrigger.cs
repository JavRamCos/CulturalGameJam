using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.takeHit(1, collision);
            }
        }
        else if (collision.gameObject.tag == "Boss")
        {
            BossController bc = collision.gameObject.GetComponent<BossController>();
            if (bc != null)
            {
                bc.TakeDamage(2);
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.name.Contains("ChaseEnemyPatrol"))
            {
                ChaseAIPatrol enemy = collision.gameObject.GetComponent<ChaseAIPatrol>();
                if (enemy != null)
                {
                    enemy.takeHit(2);
                }
            }
            else if (collision.gameObject.name.Contains("EnemyPatrol")
                || collision.gameObject.name.Contains("ShooterEnemyPatrol"))
            {
                Debug.Log("hola");
                AIPatrol enemy = collision.gameObject.GetComponent<AIPatrol>();
                if (enemy != null)
                {
                    enemy.takeHit(2);
                }
            }
            else if (collision.gameObject.name.Contains("Flying Enemy"))
            {
                EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    enemy.takeHit(2);
                }
            }
        }
        Destroy(this.gameObject);
    }
}
