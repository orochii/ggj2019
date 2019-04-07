using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorizeObject : MonoBehaviour
{
    private const string EMISSION_KEYWORD = "_EMISSION";
    private Material[] _allMaterials = null;
    private Color[] _allOriginalColors = null;
    private Color[] _allOriginalEmmisionColors = null;
    private bool[] _originalEmission = null;

    void Awake() {
        // Get all renderers.
        Renderer[] allRenderers = GetComponentsInChildren<Renderer>();
        // Get material count.
        int matCount = 0;
        foreach (Renderer r in allRenderers) matCount += r.materials.Length;
        // Create materials and original colors array.
        _allMaterials = new Material[matCount];
        _allOriginalColors = new Color[matCount];
        _allOriginalEmmisionColors = new Color[matCount];
        _originalEmission = new bool[matCount];
        int i = 0;
        foreach (Renderer r in allRenderers) {
            foreach (Material m in r.materials) {
                _allMaterials[i] = m;
                _allOriginalColors[i] = new Color(m.color.r, m.color.g, m.color.b, m.color.a);
                _originalEmission[i] = m.IsKeywordEnabled(EMISSION_KEYWORD);
                Color eCol = m.GetColor("_EmissionColor");
                _allOriginalEmmisionColors[i] = new Color(eCol.r, eCol.g, eCol.b, eCol.a);
                i++;
            }
        }
    }

    public void Pulsate(Color c, float time, int repeat) {
        StopAllCoroutines();
        StartCoroutine(PulsateCoroutine(c, time, repeat));
    }

    IEnumerator PulsateCoroutine(Color c, float time, int repeat) {
        Reset(false);
        int leftLoops = repeat;
        yield return new WaitForFixedUpdate();
        while (repeat < 0 || leftLoops >= 0) {
            float percent = 0;
            while (percent < 1) {
                percent += Time.fixedDeltaTime / time;
                float v = Hermite(Mathf.PingPong(percent, 0.5f)) * 2f;
                Color col = Color.Lerp(Color.clear, c, v);
                Colorize(col, false);
                yield return new WaitForFixedUpdate();
            }
            leftLoops--;
        }
    }

    private float Hermite(float t) {
        return -t * t * t * 2f + t * t * 3f;
    }
    
    public void Colorize(Color c, bool stopCoroutine = true) {
        if (stopCoroutine) StopAllCoroutines();
        for(int i = 0; i < _allMaterials.Length; i++) {
            Color newC = _allOriginalColors[i] + c;
            Color newE = _allOriginalEmmisionColors[i] + c;
            _allMaterials[i].color = newC;
            _allMaterials[i].SetColor("_EmissionColor", newE);
            _allMaterials[i].EnableKeyword(EMISSION_KEYWORD);
        }
    }

    public void Reset(bool stopCoroutine = true) {
        if (stopCoroutine) StopAllCoroutines();
        for (int i = 0; i < _allMaterials.Length; i++) {
            _allMaterials[i].color = _allOriginalColors[i];
            _allMaterials[i].SetColor("_EmissionColor", _allOriginalEmmisionColors[i]);
            _allMaterials[i].DisableKeyword(EMISSION_KEYWORD);
        }
    }
}
