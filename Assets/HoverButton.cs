using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.localScale = Lerping(rectTransform.localScale, new Vector3(1.2f, 1.2f, 1.2f), 0, 1);
        //new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localScale = new Vector3(1, 1, 1);
    }

    public void OnDisable()
    {
        rectTransform.localScale = new Vector3(1, 1, 1);
    }

    public Vector3 Lerping(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;

        float percentageComplete = timeSinceStarted / lerpTime;

        return Vector3.Lerp(start, end, Easing(percentageComplete));
    }

    public float Easing(float t)
    {
        return t / t;
    }
}
