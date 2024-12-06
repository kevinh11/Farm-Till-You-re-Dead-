using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour, IInteractable
{
    public GameObject dirt;
    public GameObject mediumPlant;
    public GameObject bigPlant;

    void Start()
    {
        // Ensure initial state
        dirt.SetActive(true);
        mediumPlant.SetActive(false);
        bigPlant.SetActive(false);
    }

    public void Interact()
    {
        StartCoroutine(GrowSequence());
    }

    private IEnumerator GrowSequence()
    {
        Debug.Log("is growing..");

        // Show medium plant
        dirt.SetActive(false);
        mediumPlant.SetActive(true);
        yield return new WaitForSeconds(1f);

        // Show big plant
        mediumPlant.SetActive(false);
        bigPlant.SetActive(true);
        yield return new WaitForSeconds(1f);

        // Revert back to dirt
        bigPlant.SetActive(false);
        dirt.SetActive(true);
        yield return new WaitForSeconds(1f);
    }
}