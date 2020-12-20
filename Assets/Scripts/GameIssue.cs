using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIssue : MonoBehaviour
{
    private UpdateUI updateUI;
    private GameObject sceneManager;
    private PauseManager pauseManager;

    [SerializeField]
    private GameObject victoryCanvas;

    [SerializeField]
    private GameObject victoryCanvasVR;

    private GameObject usedVictoryCanvas;

    [SerializeField]
    private GameObject defeatCanvas;

    [SerializeField]
    private GameObject defeatCanvasVR;

    private GameObject usedDefeatCanvas;

    private GameObject[] pausableObjects;

    public bool GameLost { get; set; }

    private void Start()
    {
        GameLost = false;

        pausableObjects = GameObject.FindGameObjectsWithTag("Pausable");
        sceneManager = GameObject.Find("SceneManager");

        if (sceneManager.GetComponent<SceneControl>().IsVRActivated)
        {
            usedVictoryCanvas = victoryCanvasVR;
            usedDefeatCanvas = defeatCanvasVR;
        }
        else
        {
            usedVictoryCanvas = victoryCanvas;
            usedDefeatCanvas = defeatCanvas;
        }

        updateUI = GetComponent<UpdateUI>();
        pauseManager = sceneManager.GetComponent<PauseManager>();

    }
    
    private void Update()
    {
        if (GameLost)
        {
            PauseStuffForDefeat();
        }

        if((updateUI.DingDongDitchesDone >= updateUI.GoalDingDongDitchesDone) &&
            (updateUI.EggsThrown >= updateUI.GoalEggsThrown) &&
            (updateUI.PoopBagDeposed >= updateUI.GoalPoopBagDeposed))
        {
            PauseStuffForVictory();
        }
    }


    private void PauseStuffForVictory()
    {
        foreach (GameObject pausableObject in pausableObjects)
        {
            pausableObject.SetActive(false);
        }

        usedVictoryCanvas.SetActive(true);
    }

    private void PauseStuffForDefeat()
    {
        foreach (GameObject pausableObject in pausableObjects)
        {
            pausableObject.SetActive(false);
        }

        usedDefeatCanvas.SetActive(true);
    }
}
