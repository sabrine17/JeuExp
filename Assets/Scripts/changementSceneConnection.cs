using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class changementSceneConnection : MonoBehaviour
{    
    public void ChangeScene()
    {
        SceneManager.LoadScene("Connexion");
        print("je change de scene");
    }

}
