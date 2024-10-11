using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class PlayerInteract : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 rayOrigin = InteractorSource.position;
            Vector3 rayDirection = InteractorSource.forward;
            Ray r = new Ray(rayOrigin, rayDirection);

            Debug.DrawRay(rayOrigin, rayDirection * InteractRange, Color.red, 2.0f); // Draw the ray for debugging
            Debug.Log($"Ray Origin: {rayOrigin}, Ray Direction: {rayDirection}");

            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                Debug.Log("Raycast hit: " + hitInfo.collider.gameObject.name); // Log the name of the hit object
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    Debug.Log("Interacting with: " + hitInfo.collider.gameObject.name); // Log the interaction
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