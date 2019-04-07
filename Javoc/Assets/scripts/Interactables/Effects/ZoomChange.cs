using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomChange : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform pivot;
    [SerializeField] float effectTime = 0.5f;

    private float _startingZoom = 0;
    private float _startingDist = 0;
    private Vector3 _startingRotation = new Vector3();
    private Coroutine _zoomCoroutine;
    private Coroutine _distCoroutine;
    private Coroutine _moveCoroutine;
    private Coroutine _rotaCoroutine;

    private void Start() {
        if (cam == null) cam = Camera.main;
        _startingZoom = cam.orthographicSize;
        _startingDist = cam.transform.localPosition.z;
        _startingRotation = pivot.rotation.eulerAngles;
    }

    private Vector3 StrToVector(string s) {
        if (s.Length < 2) return Vector3.zero;
        s = s.Substring(1, s.Length - 2);
        string[] values = s.Split(',');
        Vector3 v = new Vector3();
        for (int i = 0; i < values.Length && i < 3; i++) {
            v[i] = float.Parse(values[i]);
        }
        return v;
    }

    public void LerpTowardsPosition(Vector3 pos) {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        pos = pos - transform.position;
        if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
        _moveCoroutine = StartCoroutine(LerpPosition(pivot.localPosition, pos));
    }
    public void LerpTowardsPosition(string posStr) {
        LerpTowardsPosition(StrToVector(posStr));
    }

    public void RestartPosition() {
        if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
        _moveCoroutine = StartCoroutine(LerpPosition(pivot.localPosition, Vector3.zero));
    }

    public void LerpTowardsRotation(Vector3 rotAngle) {
        Quaternion rotation = Quaternion.Euler(rotAngle);
        if (_rotaCoroutine != null) StopCoroutine(_rotaCoroutine);
        _rotaCoroutine = StartCoroutine(LerpRotation(pivot.localRotation, rotation));
    }
    public void LerpTowardsRotation(string rotStr) {
        LerpTowardsRotation(StrToVector(rotStr));
    }

    public void RestartRotation() {
        LerpTowardsRotation(_startingRotation);
    }

    public void LerpTowardsZoom(float zoom) {
        float startZoom = cam.orthographicSize;
        if (_zoomCoroutine != null) StopCoroutine(_zoomCoroutine);
        _zoomCoroutine = StartCoroutine(LerpZoom(startZoom, zoom));
    }

    public void RestartZoom() {
        LerpTowardsZoom(_startingZoom);
    }

    public void LerpTowardsDist(float dist) {
        float startDist = cam.transform.localPosition.z;
        if (_distCoroutine != null) StopCoroutine(_distCoroutine);
        _distCoroutine = StartCoroutine(LerpDist(startDist, dist));
    }

    public void RestartDist() {
        LerpTowardsDist(_startingDist);
    }

    public void RestartAll() {
        RestartZoom();
        RestartDist();
        RestartPosition();
        RestartRotation();
    }

    private IEnumerator LerpPosition(Vector3 startPos, Vector3 endPos) {
        float percent = 0;
        float _effectTime = effectTime;
        while (percent < 1) {
            pivot.localPosition = Vector3.Lerp(startPos, endPos, percent);
            float increment = Time.fixedUnscaledDeltaTime / _effectTime;
            float waitTime = Time.fixedUnscaledDeltaTime * _effectTime;
            percent += increment;
            yield return new WaitForSecondsRealtime(waitTime);
        }
        pivot.transform.localPosition = endPos;
    }

    private IEnumerator LerpRotation(Quaternion start, Quaternion end) {
        float percent = 0;
        float _effectTime = effectTime;
        while (percent < 1) {
            pivot.localRotation = Quaternion.Lerp(start, end, percent);
            float increment = Time.fixedUnscaledDeltaTime / _effectTime;
            float waitTime = Time.fixedUnscaledDeltaTime * _effectTime;
            percent += increment;
            yield return new WaitForSecondsRealtime(waitTime);
        }
        pivot.transform.localRotation = end;
    }

    private IEnumerator LerpZoom(float startZoom, float endZoom) {
        float rangeZoom = endZoom - startZoom;
        float percent = 0;
        float _effectTime = effectTime;
        while (percent < 1) {
            cam.orthographicSize = (startZoom + (rangeZoom * percent));
            float increment = Time.fixedUnscaledDeltaTime / _effectTime;
            float waitTime = Time.fixedUnscaledDeltaTime * _effectTime;
            percent += increment;
            yield return new WaitForSecondsRealtime(waitTime);
        }
        cam.orthographicSize = endZoom;
    }

    private IEnumerator LerpDist(float startDist, float endDist) {
        float percent = 0;
        float _effectTime = effectTime;
        while(percent < 1) {
            float dist = Mathf.Lerp(startDist, endDist, percent);
            cam.transform.localPosition = new Vector3(0,0,dist);
            float increment = Time.fixedUnscaledDeltaTime / _effectTime;
            float waitTime = Time.fixedUnscaledDeltaTime * _effectTime;
            percent += increment;
            yield return new WaitForSecondsRealtime(waitTime);
        }
        cam.transform.localPosition = new Vector3(0, 0, endDist);
    }
}
