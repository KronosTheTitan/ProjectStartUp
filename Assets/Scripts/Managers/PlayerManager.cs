using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    [SerializeField] private Player[] players;
    void Start()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if(Hinput.gamepad[i] == null) continue;
            players[i].SetGamepad(Hinput.gamepad[i]);
        }
    }

    public void HandleVictory(Player winner)
    {
        GameOverScreen.instance.gameObject.SetActive(true);
    }
}
