using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] RectTransform interactIcon;
    private Interactable _currentInteractable;
    private bool _showingIcon;
    private Camera cameraMain;

    private void Awake() {
        cameraMain = Camera.main;
    }

    void Update() {
        Interactable currentInteractable = GetClosest();
        UpdateIconShow(currentInteractable != null);
        bool interact = Input.GetButtonDown("Jump");
        if (interact) {
            if (currentInteractable != null) {
                currentInteractable.Interact();
            }
        }
    }

    private void UpdateIconShow(bool v) {
        Debug.Log(v);
        Vector3 playerPositionScreen = cameraMain.WorldToScreenPoint(transform.position);
        interactIcon.position = playerPositionScreen;
        if (_showingIcon == v) return;
        interactIcon.gameObject.SetActive(v);
        _showingIcon = v;
    }

    Interactable GetClosest() {
        Interactable closest = null;
        float closestDistance = 0;
        foreach (Interactable i in Interactable.Interactables) {
            if (i != null) {
                float distance = (i.transform.position - transform.position).sqrMagnitude;
                if (i.CloseEnough(distance)) {
                    if (closest == null || closestDistance > distance) {
                        closest = i;
                        closestDistance = distance;
                    }
                }
            }
        }
        return closest;
    }
}
