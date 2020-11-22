using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ProgressSceneLoader : MonoBehaviour
{
    private static ProgressSceneLoader _instance;

    public static ProgressSceneLoader Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField]
    private Text progressText;

    [SerializeField]
    private Slider progressBar;

    private AsyncOperation operation;
    private Canvas canvas;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        canvas = GetComponentInChildren<Canvas>(true);
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        UpdateProgressUI(0);
        canvas.gameObject.SetActive(true);

        StartCoroutine(BeginLoad(sceneName));
    }

    private IEnumerator BeginLoad(string sceneName)
    {
        operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            UpdateProgressUI(operation.progress);
            yield return null;
        }

        UpdateProgressUI(operation.progress);
        operation = null;
        canvas.gameObject.SetActive(false);
    }
    
    private void UpdateProgressUI(float progress)
    {
        progressBar.value = progress;
        progressText.text = (int)(progress * 100f) + "%";
    }
}
