using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float maxDistance = 0;
    
    void Update() {
        bool interact = Input.GetButtonDown("Jump");
        if (interact) {
            Interactable currentInteractable = GetClosest();
            if (currentInteractable != null) {
                currentInteractable.Interact();
            }
        }
    }
    
    Interactable GetClosest() {
        Interactable closest = null;
        float closestDistance = 0;
        foreach (Interactable i in Interactable.Interactables) {
            float distance = (i.transform.position - transform.position).sqrMagnitude;
            if (distance < maxDistance) {
                if (closest == null || closestDistance > distance) {
                    closest = i;
                    closestDistance = distance;
                }
            }
        }
        return closest;
    }
}
