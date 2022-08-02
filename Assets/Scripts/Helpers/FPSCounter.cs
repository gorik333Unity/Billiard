using System.Collections;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text counterText;

    [SerializeField]
    private float _updateInterval = 0.5f;

    private void Start() => StartCoroutine(CountFPS());

    private IEnumerator CountFPS()
    {
        var count = 0;
        for (var t = 0f; t < _updateInterval; t += Time.deltaTime, count++)
            yield return null;

        counterText.SetText($"FPS: {count / _updateInterval}");
        StartCoroutine(CountFPS());
    }
}