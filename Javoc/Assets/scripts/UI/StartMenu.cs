using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private void Start() {
        AudioManager.PlayMusic("m_inicio");
    }

    private void Update() {
        if (Input.GetButtonUp("Jump")) {
            SceneManager.LoadScene("pasillo");
            AudioManager.PlayMusic("m_sala");
        }
    }
}
