using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFlash : MonoBehaviour
{
    [SerializeField] ColorizeObject obj;
    [SerializeField] float time = 1;
    [SerializeField] int repeat;
    [SerializeField] Color color;
    
    public void Colorize() {
        obj.Colorize(color);
    }
    public void Flash() {
        obj.Pulsate(color, time, repeat);
    }
}
