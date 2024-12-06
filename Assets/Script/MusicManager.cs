using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isMusicOn = true;

    private static MusicManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleMusic(bool isOn)
    {
        if (isOn)
        {
            audioSource.Play(); // Play the music
        }
        else
        {
            audioSource.Pause(); // Pause the music
        }

        isMusicOn = isOn; // Set the state
    }

    public bool IsMusicOn()
    {
        return isMusicOn;
    }
}