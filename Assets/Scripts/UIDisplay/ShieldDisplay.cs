using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShieldDisplay : MonoBehaviour
{

    TextMeshProUGUI shieldText;
    Player player;

    // Use this for initialization
    void Start()
    {
        shieldText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        shieldText.text = player.GetShieldQuantity().ToString();
    }
}
