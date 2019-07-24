using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public PlayerController player = null;
    public GameObject welcomUI = null;

    public enum Status { WELCOM, PLAYING, VICTORY, DEFEAT};
    Status status;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        status = Status.WELCOM;
        welcomUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(status == Status.WELCOM)
            {
                status = Status.PLAYING;
                welcomUI.SetActive(false);
                player.StartGame();
            }
        }
    }
}
