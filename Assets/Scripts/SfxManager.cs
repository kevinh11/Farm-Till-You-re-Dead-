using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SfxManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Slider sfxSlider;
    private AudioSource audioSource;
    public AudioClip crunchSfx;
    public AudioClip[] footstepsSfx;
    public AudioClip errorSfx;
    private float walkSfxCooldown = 0.5f;
    private bool canPlayWalk = true;
    private float lastTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (sfxSlider != null){
            sfxSlider.onValueChanged.AddListener(delegate { AdjustSfxVolume(sfxSlider.value);});
        } 

        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - lastTime) >= walkSfxCooldown) {
            canPlayWalk = true;
        }
    }

    void AdjustSfxVolume(float value)
    {
        audioSource.volume = value;
    
    }

    public void PlayWalkingSound()
    {
        if (!canPlayWalk) return;
        for (int i =0; i < footstepsSfx.Length; i++)
        {
            audioSource.clip = footstepsSfx[i];
            audioSource.Play();
        }

        lastTime = Time.time;
        canPlayWalk = false;
    }

    public void PlayErrorSound()
    {
        audioSource.clip = errorSfx;
        audioSource.Play();
    }

    public void PlayCrunchSound()
    {
        audioSource.clip = crunchSfx;
        audioSource.Play();
    }
}
