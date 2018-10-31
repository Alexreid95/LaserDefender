using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStar : MonoBehaviour
{

    // Cashed references
    Item item;
    Player player;
    GameSession gameSession;

    // Use this for initialization
    void Start()
    {
        // Calling method from script  
        item = FindObjectOfType<Item>();
        player = FindObjectOfType<Player>();
        gameSession = FindObjectOfType<GameSession>();
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
        gameSession.AddStarToScore(GetPointsForStartFromPlayer());
        Destroy(gameObject);
    }

    private int GetPointsForStartFromPlayer()
    {
        return player.PointsForStar();
    }
}
