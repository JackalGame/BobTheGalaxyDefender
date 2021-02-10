using UnityEngine;


public class Astroid : MonoBehaviour
{
    [Range (0f, 10f)] [SerializeField] float speed = 1f;

    private Rigidbody2D rigidbody;
    private Vector2 screenBounds;


    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(-speed, 0f);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }


    private void Update()
    {
        if(transform.position.x < screenBounds.x * -2)
        {
            Destroy(this.gameObject);
        }
    }

}
