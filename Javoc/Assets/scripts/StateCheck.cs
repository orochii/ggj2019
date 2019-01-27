using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateCheck : MonoBehaviour
{
    [SerializeField] int awakeFlag;
    [SerializeField] UnityEvent onAwakeTrue;
    [SerializeField] UnityEvent onAwakeFalse;

    [SerializeField] int interactFlag;
    [SerializeField] UnityEvent interactionTrue;
    [SerializeField] UnityEvent interactionFalse;

    [SerializeField] bool checkIfEnd = false;
    [SerializeField] UnityEvent onCheckEnd;

    private void Awake() {
        bool flag = GameManager.Instance.GetFlag(awakeFlag);
        if (flag) {
            if (onAwakeTrue != null) onAwakeTrue.Invoke();
        } else {
            if (onAwakeFalse != null) onAwakeFalse.Invoke();
        }
        if (checkIfEnd) CheckEnd();
    }

    public void CheckFlag() {
        bool flag = GameManager.Instance.GetFlag(interactFlag);
        if (flag) {
            if (interactionTrue != null) interactionTrue.Invoke();
        } else {
            if (interactionFalse != null) interactionFalse.Invoke();
        }
    }

    public void SetFlag(int i) {
        GameManager.Instance.SetFlag(i, true);
    }
    public void UnsetFlag(int i) {
        GameManager.Instance.SetFlag(i, false);
    }
    public void CheckEnd() {
        bool flag0 = GameManager.Instance.GetFlag(0);
        bool flag1 = GameManager.Instance.GetFlag(1);
        bool flag2 = GameManager.Instance.GetFlag(2);
        if (flag0 && flag1 && flag2) {
            GameManager.Instance.SetFlag(-1, true); // -1 es que pasó el juego.
            if (onCheckEnd != null) onCheckEnd.Invoke();
        }
    }
}
