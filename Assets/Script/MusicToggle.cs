using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public Toggle musicToggle;
    private MusicManager musicManager;

    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        musicToggle.onValueChanged.AddListener(OnToggleValueChanged);
        musicToggle.isOn = musicManager.IsMusicOn(); 
    }

    void OnToggleValueChanged(bool isOn)
    {
        musicManager.ToggleMusic(isOn);
    }
}