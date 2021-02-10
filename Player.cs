using System;
using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("Thrusters")]
    [SerializeField] float thrusterPower = 0.2f;
    [SerializeField] ParticleSystem thrusterVFX;
    [SerializeField] AudioClip thrusterSFX;
    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] GameObject gun;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float fireRate = 5f;
    
    
    float sfxVolume;
    bool isAlive = true;
    

    Rigidbody2D myRigidBody;
    CapsuleCollider2D myCollider;
    Animator myAnimator;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        myCollider = GetComponent<CapsuleCollider2D>();
        sfxVolume = PlayerPrefsController.GetMasterSFXVolume();
    }

    void Update()
    {
        if (!isAlive) { return; }

        Touch();
        Die();
        PlayerTooLow();
    }

    private void Touch()
    {
        foreach(Touch touch in Input.touches) //Enable multiple touches at same time.
        {
            if (touch.position.x > Screen.width / 2) // Touch on right side of screen.
            {
                if (touch.phase == TouchPhase.Began) // Only action when touch first initiated.
                {
                    GameObject laser = Instantiate(projectilePrefab, gun.transform.position, transform.rotation) as GameObject;
                    AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, sfxVolume);
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0); 
                }

            }
            else if (touch.position.x < Screen.width / 2) // Touch on left side of screen.
            {
                {
                    Vector2 upwardVelocityToAdd = new Vector2(0f, thrusterPower);
                    myRigidBody.velocity += upwardVelocityToAdd;
                    myAnimator.SetBool("isFlying", true);
                    thrusterVFX.Play();
                }
                if(touch.phase == TouchPhase.Ended) // Action when touch has ended.
                {
                    myAnimator.SetBool("isFlying", false);
                    thrusterVFX.Stop();
                }
            }
        }
    }


    private void PlayerTooLow()
    {
        if (transform.position.y < -6)
        {
            isAlive = false;
            FindObjectOfType<GameManager>().HandleLoseCondition();
        }
    }

    private void Die()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Asteroid", "Enemy", "EnemyLaser")))
        {
            isAlive = false;
            myRigidBody.gravityScale = 0.0f;
            myAnimator.SetTrigger("isDead");
            StartCoroutine(DisableCollider());
            StartCoroutine(PlayerHasDied());
        }
    }

    IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(0.6f);
        myCollider.enabled = false;
    }

    IEnumerator PlayerHasDied()
    {
        yield return new WaitForSeconds(2.5f);
        FindObjectOfType<GameManager>().HandleLoseCondition();
    }

    public void ChangePlayerState()
    {
        if (isAlive)
        {
            isAlive = false;
        }
        else
        {
            isAlive = true;
        }
    }

}
