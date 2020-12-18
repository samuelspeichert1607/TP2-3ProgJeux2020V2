using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;

    [SerializeField]
    private GameObject optionsParticle;

    [SerializeField]
    private GameObject optionsButton;

    private void Awake()
    {
        GetComponentInChildren<Button>().onClick.AddListener(PlayGame);
    }

    public void PlayGame()
    {
        gameObject.SetActive(false);
        FindObjectOfType<ProgressSceneLoader>().LoadScene(sceneToLoad);
    }

    public void OptionsGame()
    {
        //Instantiate(optionsParticle, optionsButton.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity);
        //optionsParticle.SetActive(true);
        optionsParticle.GetComponent<ParticleSystem>().Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}