using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EasingAnimation : MonoBehaviour
{
    public Button startButton;
    public Button optionsButton;
    public Button quitButton;

    private Vector3 startButtonInitialPosition;
    private Vector3 optionsButtonInitialPosition;
    private Vector3 quitButtonInitialPosition;

    [SerializeField]
    private Vector2 startButtonFinalPosition;

    [SerializeField]
    private Vector2 optionsButtonFinalPosition;

    [SerializeField]
    private Vector2 quitButtonFinalPosition;

    private bool shouldLerp = false;
    public float timeStartedLerping;
    public float lerpTime;

    public bool shouldReverseLerp = false;

    private void StartLerping()
    {
        timeStartedLerping = Time.time;

        shouldLerp = true;
    }

    private void Start()
    {
        startButtonInitialPosition = startButton.GetComponent<RectTransform>().anchoredPosition;
        optionsButtonInitialPosition = optionsButton.GetComponent<RectTransform>().anchoredPosition;
        quitButtonInitialPosition = quitButton.GetComponent<RectTransform>().anchoredPosition;

        /*startButtonFinalPosition = new Vector3(0, 0, 0);
        optionsButtonFinalPosition = new Vector3(0, -60, 0);
        quitButtonFinalPosition = new Vector3(0, -120, 0);*/

        StartLerping();
    }

    /*public void ComingBack()
    {
        startButton.GetComponent<RectTransform>().anchoredPosition = Lerping(startButtonFinalPosition, startButtonInitialPosition, timeStartedLerping, lerpTime);
        optionsButton.GetComponent<RectTransform>().anchoredPosition = Lerping(optionsButtonFinalPosition, optionsButtonInitialPosition, timeStartedLerping, lerpTime);
        quitButton.GetComponent<RectTransform>().anchoredPosition = Lerping(quitButtonFinalPosition, quitButtonInitialPosition, timeStartedLerping, lerpTime);
    }*/

    // Update is called once per frame
    private void Update()
    {
        if (shouldLerp)
        {
           if(startButton.GetComponent<RectTransform>().anchoredPosition.x <= startButtonFinalPosition.x - 0.07f)
           {
               startButton.GetComponent<RectTransform>().anchoredPosition = Lerping(startButtonInitialPosition, startButtonFinalPosition, timeStartedLerping, lerpTime);
               optionsButton.GetComponent<RectTransform>().anchoredPosition = Lerping(optionsButtonInitialPosition, optionsButtonFinalPosition, timeStartedLerping, lerpTime);
               quitButton.GetComponent<RectTransform>().anchoredPosition = Lerping(quitButtonInitialPosition, quitButtonFinalPosition, timeStartedLerping, lerpTime);
           }
        }
    }

    public Vector3 Lerping(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;

        float percentageComplete = timeSinceStarted / lerpTime;

        return Vector3.Lerp(start, end, Easing(percentageComplete));
    }

    public float Easing(float t)
    {
        return 0.5f * Mathf.Sin((t - 0.5f) * Mathf.PI) + 0.5f;
    }
}
