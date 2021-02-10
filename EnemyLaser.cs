using UnityEngine;


public class EnemyLaser : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
