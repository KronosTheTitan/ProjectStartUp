using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player[] players;
    void Start()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if(Hinput.gamepad[i] == null) continue;
            players[i].SetGamepad(Hinput.gamepad[i]);
        }
    }
}
