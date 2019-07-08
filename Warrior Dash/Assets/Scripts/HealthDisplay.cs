using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    //cached references
    TextMeshProUGUI healthText;
    Player player;

    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    //make the health text equal the current health
    void Update()
    {
        healthText.SetText(player.GetHealth().ToString());
    }
}