using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    private Vector3 originalPosition;
    private float shakeDuration = .2f;
    private float shakeMagnitude = 0.1f;

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
        originalPosition = transform.localPosition;
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
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.localPosition = originalPosition + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;
    }
}