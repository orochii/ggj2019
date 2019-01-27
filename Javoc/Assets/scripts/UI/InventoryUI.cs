using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject[] objects = new GameObject[0];

    private void FixedUpdate() {
        if (GameManager.Instance != null) {
            for (int i = 0; i < objects.Length; i++) {
                bool flag = GameManager.Instance.GetFlag(i);
                objects[i].SetActive(flag);
            }
        }
    }
}
