using System;
using UnityEngine;


public class EnemyShip : MonoBehaviour
{
    [Header("General")]
    [SerializeField] string alienName;
    [Range(0f, 10f)] [SerializeField] float speed = 1f;
    [SerializeField] int enemyHealth = 1;
    [SerializeField] float bonusScore = 50f;
    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject gun;
    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 2f;
    [Header("Effects")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float explosionDuration = 1f;
    

    private Rigidbody2D rigidbody;
    private Vector2 screenBounds;

    float shotCounter;
    float sfxVolume;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(-speed, 0f);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        sfxVolume = PlayerPrefsController.GetMasterSFXVolume();
    }


    private void Update()
    {
        if (transform.position.x < screenBounds.x * -2)
        {
            Destroy(this.gameObject);
        }

        CountDownAndShoot(); 
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

    public void DealDamage(int damage)
    {
        enemyHealth -= damage;
        if(enemyHealth <= 0)
        {
            UnlockAlien();
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, sfxVolume * 2);
            GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
            Destroy(explosion, explosionDuration);
            FindObjectOfType<GameManager>().AddBonusScore(bonusScore);
            AddStars();
        }
    }

    public void UnlockAlien() // Unlock Alien on Alien Data Scene.
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
    }
}
