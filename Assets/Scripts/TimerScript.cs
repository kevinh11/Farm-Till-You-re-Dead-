using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    float seconds = 60;
    int displayMinutes; 
    int displaySeconds;
    Text timerText;
    GameManagerScript managerScript;
    private PlayerInteract playerInteract;
    private GameManagerScript gameManager;

    void Start()
    {
        timerText = GetComponent<Text>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

        // playerInteract = GameObject.Find("Player").GetComponent<PlayerInteract>();
    }

    void ChangeTextFormat(float seconds)
    {
        displayMinutes = Mathf.FloorToInt(seconds / 60);
        displaySeconds = Mathf.FloorToInt(seconds % 60);

        timerText.text = displayMinutes + ":" + displaySeconds.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        seconds -= Time.deltaTime;

        if (seconds <= 0)
        {
            seconds = 0;
            gameManager.HandleVictory();

        }

        ChangeTextFormat(seconds);
    }
}
