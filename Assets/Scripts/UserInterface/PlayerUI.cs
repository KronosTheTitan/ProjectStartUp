using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text score;
    
    /// <summary>
    /// This function updates the players health bar.
    /// It should be called as part of an event when the player
    /// takes damage.
    /// </summary>
    public void UpdateHealth()
    {
        healthBar.value = player.health;
    }

    public void UpdateScore()
    {
        score.text = player.score.ToString();
    }
}
