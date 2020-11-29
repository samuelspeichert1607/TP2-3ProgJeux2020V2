using UnityEngine;
using UnityEngine.UI;

public class DiffucultyMode : MonoBehaviour
{
    [SerializeField]
    private Toggle easyButton;

    [SerializeField]
    private Toggle normalButton;

    [SerializeField]
    private Toggle hardButton;

    // Update is called once per frame
    private void Update()
    {

        if (easyButton.isOn)
        {
            normalButton.isOn = false;
            hardButton.isOn = false;
        }
        else if (normalButton.isOn)
        {
            easyButton.isOn = false;
            hardButton.isOn = false;
        }
        else if (hardButton.isOn)
        {
            normalButton.isOn = false;
            easyButton.isOn = false;
        }
    }
}
