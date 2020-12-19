using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    private TextMeshProUGUI[] uiTexts;

    public int DingDongDitchesDone { get; set; }
    public int EggsThrown { get; set; }
    public int PoopBagDeposed { get; set; }

    public int GoalDingDongDitchesDone { get; set; }
    public int GoalEggsThrown { get; set; }
    public int GoalPoopBagDeposed { get; set; }

    private bool DingDongDitchesAreFinished;
    private bool EggsThrownAreFinished;
    private bool PoopBagDeposedAreFinished;
    private AudioSource taskFinishedSound;

    // Start is called before the first frame update
    private void Start()
    {
        uiTexts = GetComponentsInChildren<TextMeshProUGUI>();

        GoalDingDongDitchesDone = 3;
        GoalEggsThrown = 3;
        GoalPoopBagDeposed = 1;

        DingDongDitchesAreFinished = false;
        EggsThrownAreFinished = false;
        PoopBagDeposedAreFinished = false;
        taskFinishedSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (TextMeshProUGUI uiText in uiTexts)
        {
            if (uiText.name == "Text DDD")
            {
                uiText.text = "Ding Dong Ditches : " + DingDongDitchesDone + "/" + GoalDingDongDitchesDone;
                if (!DingDongDitchesAreFinished && DingDongDitchesDone >= GoalDingDongDitchesDone)
                {
                    DingDongDitchesAreFinished = true;
                    taskFinishedSound.Play();
                }
            }
            if (uiText.name == "Text Eggs")
            {
                uiText.text = "Eggs : " + EggsThrown + "/" + GoalEggsThrown;
                if (!EggsThrownAreFinished && EggsThrown >= GoalEggsThrown)
                {
                    EggsThrownAreFinished = true;
                    taskFinishedSound.Play();
                }
            }
            if (uiText.name == "Text Poop")
            {
                uiText.text = "Poop bag : " + PoopBagDeposed + "/" + GoalPoopBagDeposed;
                if (!PoopBagDeposedAreFinished && PoopBagDeposed >= GoalPoopBagDeposed)
                {
                    PoopBagDeposedAreFinished = true;
                    taskFinishedSound.Play();
                }
            }
        }
    }
}
