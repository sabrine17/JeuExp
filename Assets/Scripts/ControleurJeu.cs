using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ControleurJeu : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {
        if (ConnexionServeur.j1Selectione == true)
        {

            GameObject spawn = GameObject.Find("SpawnJ1");

            PhotonNetwork.Instantiate("PersoPrincipal_1", new Vector2(spawn.transform.position.x,spawn.transform.position.y),new Quaternion(0,0,0,0),0);
            print("Ca marche");

        }

        else if (ConnexionServeur.j2Selectione == true)
        {
            GameObject spawn = GameObject.Find("SpawnJ2");
            PhotonNetwork.Instantiate("PersoSecondaire_1", new Vector2(spawn.transform.position.x, spawn.transform.position.y), new Quaternion(0, 0, 0, 0), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
