using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour, IInteractable
{
    public float growthTime;

    private bool isGrown = false;

    void Start()
    {
        GrowOverTime();

    }
    public void Interact()
    {
        if (isGrown)
        {
            StartCoroutine(GrowOverTime());
        }
    }

    void Harvest()
    {
        isGrown = false;
    }
    private IEnumerator GrowOverTime()
    {
        Debug.Log("is growing..");
        yield return new WaitForSeconds(growthTime);
        isGrown = true;
        Debug.Log("Growth complete!");
    }
}
