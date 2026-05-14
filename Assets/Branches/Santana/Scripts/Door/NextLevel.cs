using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class NextLevel : MonoBehaviour
{
    public GameObject promptObject;
    public string sceneName;
    public float fadeDuration;
    public string spawnID;

    private bool playerInRange = false;
    private Image fadeImage;

    void Start()
    {
        Debug.Log("Script iniciou");
        fadeDuration= 1f;
        if (promptObject != null)
            promptObject.SetActive(false);

        GameObject canvas = new GameObject("FadeCanvas");
        Canvas c = canvas.AddComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        c.sortingOrder = 99;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        GameObject panel = new GameObject("FadePanel");
        panel.transform.SetParent(canvas.transform, false);
        RectTransform rect = panel.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;
        fadeImage = panel.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        if (playerInRange)
        {
            Debug.Log("Player dentro da área");

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Tentando trocar de cena");
                StartCoroutine(FadeAndLoad());
            }
        }
    }

    IEnumerator FadeAndLoad()
    {
        playerInRange = false;

        SpawnManager.spawnID = spawnID;

        if (promptObject != null)
            promptObject.SetActive(false);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(elapsed / fadeDuration));
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (promptObject != null)
                promptObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (promptObject != null)
                promptObject.SetActive(false);
        }
    }
}