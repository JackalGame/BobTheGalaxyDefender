using System;
using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] float thrusterPower = 0.2f;
    [SerializeField] ParticleSystem thrusterVFX;
    [SerializeField] AudioClip thrusterSFX;
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
        foreach(Touch touch in Input.touches)
        {
            if (touch.position.x > Screen.width / 2)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    GameObject laser = Instantiate(projectilePrefab, gun.transform.position, transform.rotation) as GameObject;
                    AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, sfxVolume);
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0); 
                }

            }
            else if (touch.position.x < Screen.width / 2)
            {
                {
                    Vector2 upwardVelocityToAdd = new Vector2(0f, thrusterPower);
                    myRigidBody.velocity += upwardVelocityToAdd;
                    myAnimator.SetBool("isFlying", true);
                    thrusterVFX.Play();
                }
                if(touch.phase == TouchPhase.Ended)
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
