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
    private Transform InteractorSource;
    private GameManagerScript gameManager;
    public float InteractRange;

    private float health = 100f;

    private float lastTime = 0f;
    private float currentTime = 0f;
    private float damage = 2f;
    private bool alive = true;
    
    private Rigidbody rb;
    private GameObject healthbar; 
    private Slider healthSlider;
    private PlayerInventory playerInventory;
    

    void Start()
    {
        InteractorSource = this.transform;
        healthbar = GameObject.Find("BarImage");
        rb = GetComponent<Rigidbody>();
        healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    

    private IEnumerator LieDown()
    {
        float rotateDuration = 1f;
        float elapsedTime = 0f;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(-90f, transform.rotation.y, transform.rotation.z);

        rb.isKinematic = true; 

        while (elapsedTime < rotateDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotateDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation; 
        rb.isKinematic = false; 

        gameManager.HandleDeath();
    }

    private IEnumerator die(float phase, float seconds){
            for(int i = 0; i < phase; i++){
                if(i == phase -1){
                    transform.rotation = Quaternion.Euler(90, 0, 0);
                }
                yield return new WaitForSeconds(seconds);
            }


            gameManager.HandleDeath();


    }
    

    void ReduceHealth(float value) 
    {
        health -= value;

        if (health <= 0) 
        {
            alive = false;
            Die();
        }

        SyncHealth();
    }
    
    public void AddHealth(float value) 
    {
        health += value;
        SyncHealth();
    }

    public void Die()
    {
        Debug.Log("YOU DIED");
        StartCoroutine(LieDown());

        //tambahin code buat munculin popup disini
    
    }

    void DeathAnim()
    {   
        // float deathJump = 20f;
        // rb.AddForce(Vector3.up * deathJump);
    
        Vector3 deathRotation = new Vector3(0,0,90f);
        transform.Rotate(deathRotation);
    }

    void SyncHealth()
    {
        healthSlider.value = health/100f;
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
            healthBarColor = new Color(255, 0, 0);
        }


        healthbar.GetComponent<Image>().color = healthBarColor;
    }

   

   
    void Update()
    {
        // rb.isKinematic = true;

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

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     // rb.isKinematic = false;
        //     float startTime = 0f;
        //     float deathJump = 30f;
        //     rb.AddForce(Vector3.up * deathJump, ForceMode.Impulse);

        //     StartCoroutine(die(2.0f,2.0f));

            

           
        // }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 rayOrigin = InteractorSource.position;
            Vector3 rayDirection = InteractorSource.forward;
            Ray r = new Ray(rayOrigin, rayDirection);

    
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                    Pertumbuhan plantScript = hitInfo.collider.gameObject.GetComponent<Pertumbuhan>();

                    if (!plantScript.isReady) {
                        return;
                    }
                    plantScript.Harvest();
                    playerInventory.AddToInventory(plantScript.name, plantScript.heal);
                    
                
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