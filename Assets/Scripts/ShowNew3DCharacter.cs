using UnityEngine;
using UnityEngine.UI;

public class ShowNew3DCharacter : MonoBehaviour
{
    private Toggle[] toogles;

    [SerializeField]
    private GameObject[] avatars;

    [SerializeField]
    private GameObject location;

    private GameObject avatar1;
    
    private GameObject avatar2;

    [SerializeField]
    private GameObject colorPicker1;

    [SerializeField]
    private GameObject colorPicker2;

    [SerializeField]
    private GameObject armSlider;

    [SerializeField]
    private GameObject legSlider;

    private GlobalControl globalControl;
    
    private void Awake()
    {
        globalControl = GameObject.Find("GlobalGameController").GetComponent<GlobalControl>();
        toogles = GetComponentsInChildren<Toggle>();
        avatar1 = Instantiate(avatars[0], location.transform.position, Quaternion.identity);
        avatar1.transform.rotation = new Quaternion(0, 1, 0, 0);
        avatar1.transform.parent = gameObject.transform.parent;
        avatar1.SetActive(false);

        avatar2 = Instantiate(avatars[1], location.transform.position, Quaternion.identity);
        avatar2.transform.rotation = new Quaternion(0, 1, 0, 0);
        avatar2.transform.parent = gameObject.transform.parent;
        avatar2.SetActive(false);
    }

    private void Update()
    {
        foreach(Toggle toggle in toogles)
        {
            if (toggle.isOn)
            {
                if (toggle.name == "Robot")
                {
                    avatar1.SetActive(true);
                    avatar2.SetActive(false);
                    globalControl.Avatar1Choisi();
                }
                else if (toggle.name == "Skeleton")
                {
                    avatar2.SetActive(true);
                    avatar1.SetActive(false);
                    globalControl.Avatar2Choisi();
                }
                ChangeArms();
                ChangeLegs();
                ChangeColor();



            }
        }



    }

    private void ChangeLegs()
    {
        if (avatar1.activeSelf)
        {
            GameObject legs = GameObject.Find("Hip");
            legs.transform.localScale = new Vector3(legs.transform.localScale.x, legs.transform.localScale.y,legSlider.GetComponent<Slider>().value);
            globalControl.legSize = legs.transform.localScale;
        }
        else if (avatar2.activeSelf)
        {
            GameObject legs = GameObject.Find("Bip01_Pelvis");
            legs.transform.localScale = new Vector3(legSlider.GetComponent<Slider>().value, legs.transform.localScale.y, legs.transform.localScale.z);
            globalControl.legSize = legs.transform.localScale;
        }

    }

    private void ChangeArms()
    {
        if (avatar1.activeSelf)
        {
            GameObject arms = GameObject.Find("Ribs");
            arms.transform.localScale = new Vector3(arms.transform.localScale.x, arms.transform.localScale.y, armSlider.GetComponent<Slider>().value);
            globalControl.armSize = arms.transform.localScale;
        }
        else if (avatar2.activeSelf)
        {
            GameObject arms = GameObject.Find("Bip01_Spine");
            arms.transform.localScale = new Vector3(arms.transform.localScale.x, arms.transform.localScale.y, armSlider.GetComponent<Slider>().value);
            globalControl.armSize = arms.transform.localScale;
        }
    }

    private void ChangeColor()
    {
        if(avatar1.activeSelf)
        {
            avatar1.GetComponentInChildren<SkinnedMeshRenderer>().material.color = colorPicker1.GetComponent<FlexibleColorPicker>().color;
            avatar1.GetComponentInChildren<MeshRenderer>().material.color = colorPicker2.GetComponent<FlexibleColorPicker>().color;
            globalControl.firstColor = avatar1.GetComponentInChildren<SkinnedMeshRenderer>().material.color;
            globalControl.secondColor = avatar1.GetComponentInChildren<MeshRenderer>().material.color;
        }
        else if (avatar2.activeSelf)
        {
            avatar2.GetComponentInChildren<SkinnedMeshRenderer>().material.color = colorPicker1.GetComponent<FlexibleColorPicker>().color;
            avatar2.GetComponentInChildren<MeshRenderer>().material.color = colorPicker2.GetComponent<FlexibleColorPicker>().color;
            globalControl.firstColor = avatar2.GetComponentInChildren<SkinnedMeshRenderer>().material.color;
            globalControl.secondColor = avatar2.GetComponentInChildren<MeshRenderer>().material.color;
        }
    }
}
