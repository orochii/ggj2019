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

    private void Awake() {
        bool flag = GameManager.Instance.GetFlag(awakeFlag);
        if (flag) {
            if (onAwakeTrue != null) onAwakeTrue.Invoke();
        } else {
            if (onAwakeFalse != null) onAwakeFalse.Invoke();
        }
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
}
