using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public PlayerController player = null;

    public enum Status { WELCOM, PLAYING, VICTORY, DEFEAT};
    Status status;

    void Start()
    {
        status = Status.WELCOM;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(status == Status.WELCOM)
            {
                status = Status.PLAYING;
                player.StartGame();
            }
        }
    }
}
