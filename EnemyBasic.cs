using System.Collections;
using UnityEngine;


public class EnemyBasic : MonoBehaviour
{
    [Header("General")]
    [SerializeField] string alienName;
    [Range(0f, 10f)] [SerializeField] float speed = 1f;
    [SerializeField] int enemyHealth = 1;
    [SerializeField] float bonusScore = 50f;

    [Header("Gun")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject gun;
    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 2f;

    [Header("Effects")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] float waitToDestroyInSeconds = 2f;


    private Rigidbody2D rigidbody;
    private Vector2 screenBounds;
    private Animator myAnimator;
    private CapsuleCollider2D myCollider;

    float shotCounter;
    float sfxVolume;
    bool isAlive = true;


    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(-speed, 0f);
        myAnimator = GetComponent<Animator>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        sfxVolume = PlayerPrefsController.GetMasterSFXVolume();
        myCollider = GetComponent<CapsuleCollider2D>();
    }


    private void Update()
    {
        if (transform.position.x < screenBounds.x * -2)
        {
            Destroy(this.gameObject);
        }

        if (isAlive)
        {
            CountDownAndShoot();
        }

    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {

        if (transform.position.x < screenBounds.x)
        {
            GameObject laser = Instantiate(projectilePrefab, gun.transform.position, transform.rotation) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed, 0);
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, sfxVolume);
        }
    }

    public void DealDamage(int damage) // Called from projectle script.
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            UnlockAlien();
            myCollider.enabled = false;
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, sfxVolume * 2);
            myAnimator.SetTrigger("isDead");
            speed = 0;
            FindObjectOfType<GameManager>().AddBonusScore(bonusScore);
            isAlive = false;
            Destroy(gameObject, waitToDestroyInSeconds);
            AddStars();
        }
    }

    public void UnlockAlien() // Unlocks Alien info when destroyed.
    {
        int aliensKilled = 1;

        if (aliensKilled >= PlayerPrefs.GetInt(alienName, 0))
        {
            PlayerPrefs.SetInt(alienName, 1);
        }
    }

    public void AddStars()
    {
        int totalStars = PlayerPrefs.GetInt("Stars", 0);
        totalStars += 2;
        PlayerPrefs.SetInt("Stars", totalStars);
        Debug.Log("Stars Added");
    }
}
