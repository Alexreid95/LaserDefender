using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPill : MonoBehaviour
{

    // Cashed references
    Item item;
    Player player;

    // Use this for initialization
    void Start()
    {
        // Calling method from script  
        item = FindObjectOfType<Item>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Fail safe
        if (!item) { return; }
        ProcessPickUp();
    }

    private void ProcessPickUp()
    {
        item.PickUpSFX();
        player.AddToHealth();
        Destroy(gameObject);
    }
}
