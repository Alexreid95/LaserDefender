using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : MonoBehaviour
{
    // Cashed references
    Item item;
    Player player;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Calling method from script  
        item = FindObjectOfType<Item>();
        player = FindObjectOfType<Player>();
        // Fail safe
        if (!item) { return; }
        ProcessPickUp();
    }

    private void ProcessPickUp()
    {
        item.PickUpSFX();
        player.AddToShieldQuantity();
        Destroy(gameObject);
    }
}
