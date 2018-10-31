using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour {

    // Configuration parameters
    [SerializeField] Slider healthSlider;
    Player player;

    void Update()
    {
        HealthSliderUI();
    }


    private void HealthSliderUI()
    {
        if (player.gameObject != null)
        {
            // Set the health bar's value to the current health.
            player = FindObjectOfType<Player>();
            if (player.GetHealth() > 0)
            {
                healthSlider.value = player.GetHealth();
            }
            else
            {
                healthSlider.value = 0;
            }
        }
        else
        {
            healthSlider.value = 0;
        }
        

    }

}
