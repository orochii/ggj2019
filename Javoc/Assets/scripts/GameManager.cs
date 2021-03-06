﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class GameState {
        public bool[] obtainedGoals = new bool[50];
        public bool finished = false;

        public void Restart() {
            for (int i = 0; i < obtainedGoals.Length; i++) obtainedGoals[i] = false;
        }

        public void Set(int i, bool v) {
            obtainedGoals[i] = v;
        }

        public bool Get(int i) {
            return obtainedGoals[i];
        }
    }

    private static GameManager _Instance;
    public static GameManager Instance {
        get {
            if (_Instance == null) {
                _Instance = FindObjectOfType<GameManager>();
                if (_Instance == null) {
                    GameObject go = new GameObject("GameManager");
                    _Instance = go.AddComponent<GameManager>();
                }
            }
            return _Instance;
        }
    }
    [SerializeField] GameState state;
    public bool PlayerCanMove = true;
    public bool PlayerCanInteract = true;
    
    private void Awake() {
        if (state != null) state.Restart();
        if (_Instance != null && _Instance != this) {
            Destroy(gameObject);
            return;
        }
        PlayerCanMove = true;
        PlayerCanInteract = true;
        _Instance = this;
        state = new GameState();
        DontDestroyOnLoad(gameObject);
    }

    public bool GetFlag(int i) {
        if (i < 0) return state.finished;
        return state.Get(i);
    }
    public void SetFlag(int i, bool v) {
        if (i < 0) state.finished = v;
        else state.Set(i, v);
    }
}
