using UnityEngine;


public class LaserShredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Destroy(otherCollider.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        Destroy(otherCollider.gameObject);
    }

}
