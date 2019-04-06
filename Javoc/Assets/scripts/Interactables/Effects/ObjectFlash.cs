using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFlash : MonoBehaviour
{
    [SerializeField] ColorizeObject obj;
    [SerializeField] float time = 1;
    [SerializeField] int repeat;
    [SerializeField] Color color;
    
    public void Colorize(string colStr) {
        Color c = GetColorFromString(colStr);
        obj.Colorize(c);
    }
    public void Flash(string colStr) {
        Color c = GetColorFromString(colStr);
        obj.Pulsate(c, time, repeat);
    }

    public Color GetColorFromString(string colStr) {
        Color _c;
        bool s = ColorUtility.TryParseHtmlString(colStr, out _c);
        if (!s) _c = color;
        return _c;
    }
}
