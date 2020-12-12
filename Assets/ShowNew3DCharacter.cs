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

    private void Awake()
    {
        toogles = GetComponentsInChildren<Toggle>();
        avatar1 = Instantiate(avatars[0], location.transform.position, Quaternion.identity);
        avatar1.SetActive(false);

        avatar2 = Instantiate(avatars[1], location.transform.position, Quaternion.identity);
        avatar2.SetActive(false);
        /*Instantiate(avatars[0], location.transform.position, Quaternion.identity);
        Instantiate(avatars[0], location.transform.position, Quaternion.identity);
        Instantiate(avatars[0], location.transform.position, Quaternion.identity);*/
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
                }
                else if (toggle.name == "Shrek")
                {
                    avatar2.SetActive(true);
                    avatar1.SetActive(false);
                }
            }
        }
    }
}
