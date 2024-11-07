using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IInteractable
{
    void Interact();
}

public class PlayerInteract : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    private float health = 100f;

    private float lastTime = 0f;
    private float currentTime = 0f;
    private float damage = 10f;

    private bool alive = true;
    

    public GameObject healthbar; 
    

    void ReduceHealth(float value) 
    {
        health -= value;

        if (health <= 0) {
            alive = false;
        }

        SyncHealth();

        // Debug.Log("health" + health);
    }
    
    public void AddHealth(float value) 
    {
        health += value;
        SyncHealth();
    }


    void SyncHealth()
    {
        healthbar.GetComponent<Slider>().value = health/100f;

        Color healthBarColor;

        if (health >= 66f) 
        {
            healthBarColor = new Color(0, 255 , 0);
        }

        else if (health >= 33 && health <= 66)
        {
            healthBarColor = new Color(255, 255, 0);
        }

        else 
        {
            healthBarColor = new Color(255, 255, 0);
        }


        healthbar.transform.GetChild(0).GetComponent<Image>().color = healthBarColor;

        
    }
   
    void Update()
    {

        if (!alive) {
            Debug.Log("Game Over");
            return;
        }

        currentTime += Time.deltaTime;
        if (currentTime - lastTime >= 1.0f)
        {
            ReduceHealth(damage);
            lastTime = currentTime;
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 rayOrigin = InteractorSource.position;
            Vector3 rayDirection = InteractorSource.forward;
            Ray r = new Ray(rayOrigin, rayDirection);

            
            Debug.Log($"Ray Origin: {rayOrigin}, Ray Direction: {rayDirection}");

            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    Debug.Log("Interacting with: " + hitInfo.collider.gameObject.name); 
                    interactObj.Interact();
                }
                else
                {
                    Debug.Log("No IInteractable component found on: " + hitInfo.collider.gameObject.name);
                }
            }
            else
            {
                Debug.Log("Raycast did not hit anything.");
            }
        }
    }
}