﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public void SetCanMove(bool v) {
        GameManager.Instance.PlayerCanMove = v;
    }

    public void SetCanInteract(bool v) {
        StartCoroutine(SetCanInteractNextFrame(v));
    }

    public IEnumerator SetCanInteractNextFrame(bool v) {
        yield return null;
        GameManager.Instance.PlayerCanInteract = v;
    }
}
