using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkSprite : MonoBehaviour
{
    [SerializeField] float blinkDuration = 1f;
    [SerializeField] Image img = null;

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(DoBlink());
    }

    IEnumerator DoBlink() {
        float percent = 0;
        Color currentColor = Color.white;
        Color targetColor = Color.clear;
        bool appear = false;
        while (true) {
            percent += Time.deltaTime / blinkDuration;
            if (percent < 1) {
                img.color = Color.Lerp(currentColor, targetColor, percent);
            } else {
                appear = !appear; // Flip state.
                // Change current and target colors.
                if (appear) {
                    currentColor = Color.clear;
                    targetColor = Color.white;
                } else {
                    currentColor = Color.white;
                    targetColor = Color.clear;
                }
                percent = 0;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
