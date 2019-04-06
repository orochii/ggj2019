using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractZone : MonoBehaviour
{
    [SerializeField] UnityEvent eventos;

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Interact zone: " + name);
        if (eventos != null) eventos.Invoke();
    }
}
