using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRandomizer : MonoBehaviour
{
    [SerializeField] string[] randomMaps = null;
    
    public void RandomizeTeleport(Door target) {
        if (randomMaps == null || randomMaps.Length < 1) return;
        int rndIndex = UnityEngine.Random.Range(0, randomMaps.Length);
        string sceneName = randomMaps[rndIndex];
        target.Set(sceneName, -1);
    }
}
