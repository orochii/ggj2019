using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {
    private static List<Interactable> _interactables;
    public static List<Interactable> Interactables {
        get {
            if (_interactables == null) _interactables = new List<Interactable>();
            return _interactables;
        }
    }

    [SerializeField] UnityEvent eventos;

    public void Awake() {
        Interactables.Add(this);
    }

    public void Interact() {
        if (eventos != null) eventos.Invoke();
    }
}
