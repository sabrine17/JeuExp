using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ControleurJeu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if (ConnexionServeur.j1Selectione == true)
        {
             PhotonNetwork.Instantiate("PersoPrincipal_1", new Vector2(0,0),new Quaternion(0,0,0,0),0);
        }

        else if (ConnexionServeur.j2Selectione == true)
        {
            PhotonNetwork.Instantiate("PersoSecondaire_1", new Vector2(0, 0), new Quaternion(0, 0, 0, 0), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
