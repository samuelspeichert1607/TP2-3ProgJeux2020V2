using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

public class PauseMenu : MonoBehaviourPunCallbacks
{
    public void ReturnToMenuGame()
    {
        Debug.Log("ReturnToMenuGame");
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(0);
    }
}
