using System;
using UnityEngine;


public class PlayerLaser : MonoBehaviour
{
    [SerializeField] int damage = 1;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        var enemyShip = otherCollider.GetComponent<EnemyShip>();
        if (enemyShip)
        {
            enemyShip.DealDamage(damage);
            Destroy(gameObject);
        }

        var enemyBasic = otherCollider.GetComponent<EnemyBasic>();
        if (enemyBasic)
        {
            enemyBasic.DealDamage(damage);
            Destroy(gameObject);
        }

    }

}
