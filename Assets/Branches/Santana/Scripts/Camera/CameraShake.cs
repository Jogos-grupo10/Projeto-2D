using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    private float shakeDuration = .05f;
    private float shakeMagnitude;

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }

    public void Shake(float magnitude)
    {
        
        shakeMagnitude = magnitude;
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine());
    }

    private System.Collections.IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-.5f, .5f) * shakeMagnitude;
            float y = Random.Range(-.5f, .5f) * shakeMagnitude;
            transform.localPosition += new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}