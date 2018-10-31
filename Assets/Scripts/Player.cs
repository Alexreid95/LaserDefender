using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration parameters
    [Header("Player")]
    [Range(1, 50)] [SerializeField] float moveSpeed = 10f;
    [SerializeField] float screenBoundaryPadding = 0.5f;
    [SerializeField] int health = 1000;
    [SerializeField]  int maxHealth = 1000;

    [Header("Hit SFX")]
    [SerializeField] AudioClip hitSFX;
    [SerializeField] [Range(0, 1)] float hitSFXVolume = 0.1f;

    [Header("Death SFX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 1;

    [Header("Death VFX")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("Projectile Speed")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] [Range(0, 1)] float laserSFXVolume = 0.25f;
    [SerializeField] float projectileSpeed = 12f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [Header("Special Projectile")]
    [SerializeField] GameObject laserSpecialPrefab;
    [SerializeField] AudioClip missileSFX;
    [SerializeField] [Range(0, 1)] float missileSFXVolume = 0.3f;
    [SerializeField] float specialProjectileSpeed = 8f;
    [SerializeField] float specialProjectileFiringPeriod = 1f;

    [Header("Shield Item")]
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] int shieldQuantity = 3;
    [SerializeField] float shieldDuration = 5f;
    [SerializeField] AudioClip shieldUpSFX;
    [SerializeField] [Range(0, 1)] float shieldUpSFXVolume = 0.75f;
    [SerializeField] AudioClip shieldDownSFX;
    [SerializeField] [Range(0, 1)] float shieldDownSFXVolume = 0.75f;
    [SerializeField] AudioClip errorSFX;
    [SerializeField] [Range(0, 1)] float errorSFXVolume = 0.75f;

    [Header("Pill Item")]
    [SerializeField] int pillHealingPower = 500;

    [Header("Star Item")]
    [SerializeField] int pointsForStar = 500;

    Coroutine firingCoroutine;
    bool isFiring = false;
    bool isShieldUp = false;

    float xMin;
    float xMax;
    float yMin;
    float yMax;


	// Use this for initialization
	void Start ()
    {
        SetUpMoveBoundaries();
	}


    // Update is called once per frame
    void Update ()
    {
        Move();
        Fire();
        UseItemShield();
    }

    // When Tigger is called on Collider 2D 
    private void OnTriggerEnter2D(Collider2D other)
    {
        CompleteHit(other);
    }

    
    private void CompleteHit(Collider2D other)
    {
        // Calling method from script DamageDealer 
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        // Fail safe
        if (!damageDealer) { return; }
        // calling method with param damageDealer  
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, hitSFXVolume);
        // Inflicts damage to the player 
        health -= damageDealer.GetDamage();
        // Destroys the object that hit the player
        damageDealer.Hit();

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public int AddToHealth()
    {
        // adds health to the player
        if (health < (maxHealth - pillHealingPower))
        {
            health += pillHealingPower;
        }

        else
        {
            health = maxHealth;
        }

        return health;
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation) as GameObject;
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        Destroy(explosion, durationOfExplosion);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1") && !isFiring)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
            isFiring = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
            isFiring = false;
        }

        if (Input.GetButtonDown("Fire2") && !isFiring)
        {
            firingCoroutine = StartCoroutine(SpecialFireContinuously());
            isFiring = true;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            StopCoroutine(firingCoroutine);
            isFiring = false;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserSFXVolume);

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
        
    }

    IEnumerator SpecialFireContinuously()
    {
        while (true)
        {
            GameObject laserSpecial = Instantiate(laserSpecialPrefab, transform.position, Quaternion.identity) as GameObject;
            laserSpecial.GetComponent<Rigidbody2D>().velocity = new Vector2(0, specialProjectileSpeed);
            AudioSource.PlayClipAtPoint(missileSFX, Camera.main.transform.position, missileSFXVolume);

            yield return new WaitForSeconds(specialProjectileFiringPeriod);
        }

    }


    private void UseItemShield()
    {
        if (Input.GetButtonDown("Fire3") && !isShieldUp)
        {

            if (shieldQuantity <= 0)
            {
                AudioSource.PlayClipAtPoint(errorSFX, Camera.main.transform.position, errorSFXVolume);
            }

            else
            {
                shieldQuantity -= 1;
                StartCoroutine(ShieldUp());
            }
        }

    }


    IEnumerator ShieldUp()
    {
        isShieldUp = true;
        GameObject shield = Instantiate(shieldPrefab, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(shieldUpSFX, Camera.main.transform.position, shieldUpSFXVolume);

        yield return new WaitForSeconds(shieldDuration);

        Destroy(shield);
        isShieldUp = false;
        AudioSource.PlayClipAtPoint(shieldDownSFX, Camera.main.transform.position, shieldDownSFXVolume);

    }

    public int GetShieldQuantity()
    {
        return shieldQuantity;
    }

    public int AddToShieldQuantity()
    {
        shieldQuantity++;
        return shieldQuantity;
    }

    public int PointsForStar()
    {
        return pointsForStar;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + screenBoundaryPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - screenBoundaryPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + screenBoundaryPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - screenBoundaryPadding;
    }
}
