using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pertumbuhan : MonoBehaviour
{
    public GameObject[] plants;
    private IEnumerator prosesPertumbuhanCoroutine;
    private int currentProses = 0; 

    private MeshRenderer sickleIconRenderer;
    private Slider progressSlider;
    private GameObject progressBar;

    public bool isReady = false;
    new public string name;

    public int heal;

    public float growthTime;

    void Start()
    {

        sickleIconRenderer = transform.Find("Sickle").GetComponent<MeshRenderer>();
        progressBar = transform.Find("ProgressBar").gameObject;
        progressSlider = progressBar.transform.Find("Canvas").transform.Find("ProgressSlider").gameObject.GetComponent<Slider>();

        sickleIconRenderer.enabled = false;
        foreach (GameObject plant in plants)
        {
            plant.SetActive(false);
        }

        prosesPertumbuhanCoroutine = PertumbuhanProses(growthTime);
        StartCoroutine(prosesPertumbuhanCoroutine); 
        
    }

    public void Harvest()
    {
        if (!isReady){
            Debug.Log(name + "is not ready to be harvested!");
            return;
        }


        foreach (GameObject plant in plants)
        {
            plant.SetActive(false);
        }

        sickleIconRenderer.enabled = false;
        progressBar.SetActive(true);
        progressSlider.value = 0;

        currentProses = 0;
        prosesPertumbuhanCoroutine = PertumbuhanProses(growthTime);
        StartCoroutine(prosesPertumbuhanCoroutine); 

        
    }
    void Update()
    {
        
       
       
    }

    private IEnumerator PertumbuhanProses(float time)
    {
        isReady = false;

        // Total number of plants for progress calculation
        float totalPlants = plants.Length;

        while (currentProses < plants.Length)
        {
            GameObject obj = plants[currentProses];
            obj.SetActive(true);

           
            float elapsedTime = 0f;
            float startProgress = progressSlider.value; 
            float endProgress = (currentProses + 1) / totalPlants; 

            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                progressSlider.value = Mathf.Lerp(startProgress, endProgress, elapsedTime / time);
                yield return null;
            }

            if (currentProses != plants.Length - 1)
            {
                obj.SetActive(false);
            }

            currentProses++; 
        }

        Debug.Log(name + " is Ready to be harvested!");
        isReady = true;

        progressBar.SetActive(false);
        sickleIconRenderer.enabled = true;

        // Reset the coroutine reference when done
        prosesPertumbuhanCoroutine = null;
    }

}
