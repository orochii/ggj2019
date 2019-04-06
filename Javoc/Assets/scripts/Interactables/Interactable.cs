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
    [SerializeField] float maxDistance;

    // Remueve todas las referencias perdidas del array.
    public static void Depurar() {
        Interactable[] todos = _interactables.ToArray();
        _interactables.Clear();
        foreach (Interactable i in todos) {
            if (i != null) _interactables.Add(i);
        }
    }

    public bool CloseEnough(float d) {
        if (!gameObject.activeInHierarchy) return false;
        return maxDistance > d;
    }

    public void Awake() {
        Interactables.Add(this);
    }

    public void Interact() {
        if (eventos != null) eventos.Invoke();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
