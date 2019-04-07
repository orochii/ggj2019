using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    // Static members for universal access.
    private static int NextDoorId = -1;

    private static Door GetDoorById(int id) {
        Door[] doors = GameObject.FindObjectsOfType<Door>();
        foreach (Door door in doors) {
            if (door.doorId == id) {
                return door;
            }
        }
        return null;
    }
    public static void LocateNextDoor(Transform player) {
        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.identity;
        Door door = GetDoorById(NextDoorId);
        if (door != null) {
            position = door.transform.position;
            rotation = door.transform.rotation;
        }
        player.position = position + Vector3.up * .7f;
        player.rotation = rotation;
        NextDoorId = -1;
    }

    // Instance behavior members.

    [SerializeField] int doorId;
    [SerializeField] string targetSceneName;
    [SerializeField] int nextDoorId;

    public void Teleport() {
        NextDoorId = nextDoorId;
        SceneManager.LoadScene(targetSceneName);
    }
    
    public void Set(string _sceneName, int _nextDoor) {
        targetSceneName = _sceneName;
        nextDoorId = _nextDoor;
    }
}
