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
        interactIcon.gameObject.SetActive(false);
    }

    void Update() {
        _currentInteractable = GetClosest();
        UpdateIconShow(_currentInteractable != null && GameManager.Instance.PlayerCanInteract);
        bool interact = Input.GetButtonDown("Jump");
        if (interact && GameManager.Instance.PlayerCanInteract) {
            if (_currentInteractable != null) {
                _currentInteractable.Interact();
            }
        }
    }

    private void UpdateIconShow(bool v) {
        if (v) {
            Vector3 iconPositionScreen = cameraMain.WorldToScreenPoint(_currentInteractable.transform.position);
            interactIcon.position = iconPositionScreen;
        }
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
