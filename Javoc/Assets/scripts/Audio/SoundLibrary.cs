using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "soundLibrary", menuName = "Database/Create sound library")]
public class SoundLibrary : ScriptableObject {
    [System.Serializable]
    public class SoundEntry {
        public string name;
        public AudioClip[] clips;
    }

    [SerializeField] private SoundEntry[] entries;

    public AudioClip GetClipFromName(string name) {
        foreach(SoundEntry e in entries) {
            if (e.name.Equals(name)) {
                int r = Random.Range(0, e.clips.Length);
                return e.clips[r];
            }
        }
        return null;
    }
    
}
