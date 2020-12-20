﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;

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
        gameObject.SetActive(false);
        FindObjectOfType<ProgressSceneLoader>().LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}