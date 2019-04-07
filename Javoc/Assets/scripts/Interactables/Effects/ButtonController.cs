using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private UnityEvent onDeactivate;
    [SerializeField] private UnityEvent onSolve;
    [SerializeField] private Material[] materials;
    [SerializeField] private MeshRenderer[] targetRenderers;
    [SerializeField] private Transform[] buttons;
    [SerializeField] private Vector3 buttonDisplacement;
    [SerializeField] private float moveDuration;
    private int[] numbers;
    private int _numIndex;
    private float _horzCached;
    private float _vertCached;
    private bool _justPressed;
    [SerializeField] private bool state;

    void Start() {
        // Generate a random order
        int[] allIndexes = new int[materials.Length];
        for (int i = 0; i < allIndexes.Length; i++) allIndexes[i] = i;
        List<int> available = new List<int>(allIndexes);
        numbers = available.OrderBy(item => (UnityEngine.Random.value * 1000)).ToArray();
        // Assign material to each renderer.
        for (int i = 0; i < targetRenderers.Length; i++) {
            int idx = i % numbers.Length;
            targetRenderers[i].material = materials[numbers[idx]];
        }
    }

    void Update() {
        if (!state) return;
        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        if (horz != _horzCached || vert != _vertCached) {
            if (horz != 0 || vert != 0) StopAllCoroutines();
            if (horz < 0) StartCoroutine(PressButton(0));
            else if (horz > 0) StartCoroutine(PressButton(1));
            else if (vert < 0) StartCoroutine(PressButton(2));
            else if (vert > 0) StartCoroutine(PressButton(3));
            _horzCached = horz;
            _vertCached = vert;
        }
        if (Input.GetButtonUp("Jump") && !_justPressed) {
            Deactivate();
        }
        _justPressed = Input.GetButtonUp("Jump");
    }

    private void Solve() {
        if (onSolve != null) onSolve.Invoke();
        _numIndex = 0;
        Deactivate();
    }
    private void Deactivate() {
        state = false;
        if (onDeactivate != null) onDeactivate.Invoke();
    }

    IEnumerator PressButton(int n) {
        ResetButtonPositions();
        buttons[n].localPosition = buttonDisplacement;
        CheckButton(n);
        float percent = 0;
        while (percent < 1) {
            buttons[n].localPosition = Vector3.Lerp(buttonDisplacement, Vector3.zero, percent);
            percent += Time.fixedDeltaTime / moveDuration;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        buttons[n].localPosition = Vector3.zero;
    }

    void CheckButton(int n) {
        // Check if the thrown number is correct.
        int cN = numbers[_numIndex];
        if (n == cN) _numIndex++;
        else _numIndex = 0;
        // If _numIndex is higher than numbers length, mark as solved.
        if (_numIndex >= numbers.Length) {
            Solve();
            //
            AudioManager.PlaySound2D("s_puzzleComplete");
        } else if (_numIndex == 0) {
            AudioManager.PlaySound("s_puzzleFail", transform.position);
        } else AudioManager.PlaySound("s_puzzleRight", transform.position);
    }

    void ResetButtonPositions() {
        foreach (Transform b in buttons) b.localPosition = Vector3.zero;
    }

    public void Activate() {
        state = true;
        _justPressed = Input.GetButtonUp("Jump");
    }

    public void PrintSomething(string message) {
        Debug.Log(message);
    }
}
