using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PressEnterToPlay();
        }
    }

    public void PressEnterToPlay()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
