using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{   
    public Slider musicSlider;
    private AudioSource bgMusic;
    private bool isMusicOn = true;

    private static MusicManager instance;

    private Slider volumeSlider;

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
        bgMusic = GetComponent<AudioSource>();

         if (musicSlider != null){
            musicSlider.onValueChanged.AddListener(delegate { AdjustBgVolume(musicSlider.value);});
        } 

        // if (SceneManager.GetActiveScene().name == "Ui Menu") return;

        // volumeSlider = GameObject.Find("MusicSlider").transform.Find("Slider").GetComponent<Slider>();
        // volumeSlider.onValueChanged.AddListener((value)=> AdjustBgVolume(value));       
    }

    public void AdjustBgVolume(float volume)
    {
        bgMusic.volume = volume;
    }

    public void ToggleMusic(bool isOn)
    {
        if (isOn)
        {
            bgMusic.Play(); // Play the music
        }
        else
        {
            bgMusic.Pause(); // Pause the music
        }

     
        isMusicOn = isOn; // Set the state
    }

    public bool IsMusicOn()
    {
        return isMusicOn;
    }
}