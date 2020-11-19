using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ReturnToMenuGame()
    {
        Debug.Log("ReturnToMenuGame");
        SceneManager.LoadScene("Menu");
    }
}
