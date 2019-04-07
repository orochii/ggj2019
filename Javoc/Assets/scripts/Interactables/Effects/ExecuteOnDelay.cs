using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExecuteOnDelay : MonoBehaviour {
    [SerializeField] UnityEvent evento;
    [SerializeField] float tiempo;

    public void Execute() {
        StartCoroutine(DoExecute());
    }

    IEnumerator DoExecute() {
        yield return new WaitForSeconds(tiempo);
        if (evento != null) evento.Invoke();
    }
}
