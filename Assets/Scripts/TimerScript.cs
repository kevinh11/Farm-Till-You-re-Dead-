using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    float seconds = 300;
    int displayMinutes; 
    int displaySeconds;
    TextMeshProUGUI timerText;
    GameManagerScript managerScript;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
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
            // You might want to add logic here to handle the timer reaching zero
        }

        ChangeTextFormat(seconds);
    }
}
