using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Configyration parameters
    [Header("Enemy Stats")]
    [SerializeField] float health = 500f;
    [SerializeField] int pointsForHit = 12;
    [SerializeField] int pointsForDeath = 85;
    

    [Header("Hit SFX")]
    [SerializeField] AudioClip hitSFX;
    [SerializeField] [Range(0, 1)] float hitSFXVolume = 0.1f;


    [Header("Death SFX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.5f;

    [Header("Death VFX")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplotion = 1f;

    [Header("Shooting")]
    float shotCounter;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float minTimeBetweenShoots = 0.2f;
    [SerializeField] float maxTimeBetweenShoots = 3f;

    [Header("Prjectile SFX")]
    [SerializeField] AudioClip projectileSFX;
    [SerializeField] [Range(0, 1)] float projectileSFXVolume = 0.5f;

    [Header("Prjectile Player Lock")]
    [SerializeField] bool firingLockEnabled = false;
    [SerializeField] float xPosRandomFactor = 0.1f;
    float xPos;
    float xMinPos;
    float xMaxPos;

    
    GameSession gameSession;
    Player player;


    // Use this for initialization
    void Start ()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShoots, maxTimeBetweenShoots);
        // Calling methodes from  another script
        gameSession = FindObjectOfType<GameSession>();
        player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CountDownAndShoot();
	}

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShoots, maxTimeBetweenShoots);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Calling a method from another script
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer)
        {
            Die();
        }
        else
        {
            // Calling a method within the script
            ProcessHit(damageDealer);
        }

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, hitSFXVolume);
        gameSession.AddHitToScore(pointsForHit);
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameSession.AddDeathToScore(pointsForDeath);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation) as GameObject;
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        Destroy(explosion, durationOfExplotion);
    }

    // bool that toggels in unity engine that allows firing lock 
    public bool FiringLockEnabled()
    {
        return firingLockEnabled;
    }

    public float GetXPos()
    {
        
        if (FiringLockEnabled())
        {
            // Fail safe, if Player has died, position.x is 0
            if (!player)
            {
                return 0;
            }

            else
            {
                // Gets the X Postion of the player so it fires in that direction
                xMinPos = (-transform.position.x + player.transform.position.x) - xPosRandomFactor;
                xMaxPos = (-transform.position.x + player.transform.position.x) + xPosRandomFactor;
                xPos = UnityEngine.Random.Range(xMinPos, xMaxPos);

                return xPos;
            }

        }

        else
        {
            return 0;
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(GetXPos(), -projectileSpeed);
        AudioSource.PlayClipAtPoint(projectileSFX, Camera.main.transform.position, projectileSFXVolume);
    }

}
