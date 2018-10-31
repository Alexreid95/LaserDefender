using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Configuration parameters
    [Header("Block SFX")]
    [SerializeField] AudioClip blockHitSFX;
    [SerializeField] [Range(0, 1)] float blockHitSFXVolume = 0.5f;

    //Cashed component references
    Player player;


    // Use this for initialization
    void Start ()
    {
        // Calling player to access methodes in Player 
        player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        LockShieldToPlayer();
	}

    private void LockShieldToPlayer()
    {
        Vector2 shieldPos = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.position = shieldPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Calling method from script DamageDealer 
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        // Fail safe
        if (!damageDealer) { return; }
        // calling method with param damageDealer  
        BlockHit(damageDealer);
    }

    private void BlockHit(DamageDealer damageDealer)
    {
        AudioSource.PlayClipAtPoint(blockHitSFX, Camera.main.transform.position, blockHitSFXVolume);
        // Destroys the object that hit the shield
        damageDealer.Hit();
    }




}
