using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    TextMeshProUGUI healthText;
    Player player;


	// Use this for initialization
	void Start ()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Sets the text value to the currect health
        healthText.text = player.GetHealth().ToString();
        // Set the health bar's value to the current health.
        healthSlider.value = player.GetHealth();
    }

}
