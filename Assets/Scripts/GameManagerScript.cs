using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update

    bool isRunning;
    bool isPaused;
    bool settingIsOpened = false; 

    private GameObject settingsTab;
    private GameObject deathTab;
    private GameObject winTab;
    private Button settingsIcon;

  
        // private SceneManager sceneManager;
    private Button backButton;

    private MusicManager musicManager;


    void Start()
    {

        Time.timeScale = 1;
        settingsTab = GameObject.Find("SettingsTab");
        deathTab = GameObject.Find("DeathTab");
        winTab = GameObject.Find("WinTab");
        settingsIcon = GameObject.Find("SettingsIcon").GetComponent<Button>();
        backButton = GameObject.Find("BackToMenu").GetComponent<Button>();
        

        settingsTab.SetActive(settingIsOpened);
        deathTab.SetActive(false);
        winTab.SetActive(false);
        settingsIcon.onClick.AddListener(ToggleSettings);
        // sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        // backButton.onCli2ck.AddListener(()=> sceneManager.LoadScene());
    }

    void BackToMenu()
    {

    }
  

    // Update is called once per frame

    public void HandleDeath()
    {
        Time.timeScale = 0f;
        deathTab.SetActive(true);
    }

    public void HandleVictory()
    {
        Time.timeScale=0f;
        winTab.SetActive(true);
    }

    public void ToggleSettings()
    {
        settingIsOpened = !settingIsOpened;
        settingsTab.SetActive(settingIsOpened);

        if (settingIsOpened) Time.timeScale = 0;
        else Time.timeScale = 1;
    }


    void Update()
    {
        
    }
}
