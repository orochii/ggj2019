using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCurrentGroup : MonoBehaviour
{
    [SerializeField] GameObject[] gruposDeLuz = null;
    [SerializeField] float intervalo = 4f;
    private int grupoActual;

    private void Start()
    {
        grupoActual = 0;
    }

    private void SetGrupoActual(int index) {
        if (index < 0 || index > gruposDeLuz.Length) return;

    }

    void Update()
    {
        
    }
}
