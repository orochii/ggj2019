using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnEnter : MonoBehaviour
{
    [SerializeField] string songName = null;

    void Start() {
        AudioManager.PlayMusic(songName);
    }
}
