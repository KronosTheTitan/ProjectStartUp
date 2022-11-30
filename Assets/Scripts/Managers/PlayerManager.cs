using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player[] players;
    void Start()
    {
        int iterations = players.Length > Hinput.gamepad.Count ? Hinput.gamepad.Count : players.Length;
        for (int i = 0; i < iterations; i++)
        {
            players[i].SetGamepad(Hinput.gamepad[i]);
        }
    }
}
