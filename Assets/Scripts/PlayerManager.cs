using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int iterations = players.Length > Hinput.gamepad.Count ? Hinput.gamepad.Count : players.Length;
        for (int i = 0; i < iterations; i++)
        {
            players[i].SetGamepad(Hinput.gamepad[i]);
        }
    }

    [SerializeField] private Player[] players;

    // Update is called once per frame
    void Update()
    {
    }
}
