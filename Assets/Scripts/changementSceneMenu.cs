using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;

public class changementSceneMenu : MonoBehaviourPunCallbacks
{
   public void ChangeScene()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu");
        print("je change de scene");
    }
}
