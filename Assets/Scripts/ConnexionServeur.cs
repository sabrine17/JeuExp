using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ConnexionServeur : MonoBehaviourPunCallbacks
{

    public InputField inputNomSalle;
    public InputField inputNomJoueur;

    public GameObject ecranSalle;
    public GameObject ecranNom;
    public GameObject ecranChoixPerso;

    bool j1Selectione;
    bool j2Selectione;

    public GameObject imgLockJ1;
    public GameObject imgLockJ2;
    public GameObject txtJoueurConnectes;


    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connect� " + PhotonNetwork.IsConnected);
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("le joueur a d�connect� " + cause.ToString());
    }

    public override void OnJoinedLobby()
    {
        ecranNom.SetActive(true);
    }

    public override void OnCreatedRoom()
    {
        print(PhotonNetwork.CurrentRoom.Name);
        ecranSalle.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        print(PhotonNetwork.CurrentRoom.Name);
        ecranSalle.SetActive(false);
        ecranChoixPerso.SetActive(true);
        
        if (PhotonNetwork.LocalPlayer.IsMasterClient == true) {
            j1Selectione = true;
        }

        else {
            j2Selectione = true;
        }

        print(j1Selectione);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (j1Selectione) {
            imgLockJ1.SetActive(true);
        }

        else {
            imgLockJ1.SetActive(false);
        }

        if (j2Selectione) {
            imgLockJ2.SetActive(true);
        }

        else {
            imgLockJ2.SetActive(false);
        }

        if (ecranChoixPerso.activeSelf) {
            txtJoueurConnectes.GetComponent<Text>().text = "Joueurs connectés " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ecranChoixPerso.activeSelf)
            {
                PhotonNetwork.LeaveRoom();
                ecranChoixPerso.SetActive(false);
                ecranSalle.SetActive(true);
            }

            else if (ecranSalle.activeSelf)
            {
                PhotonNetwork.LeaveLobby();
                ecranSalle.SetActive(false);
                ecranNom.SetActive(true);
            }
        }

    }

    public void CreerSalle()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(inputNomSalle.GetComponent<InputField>().text, roomOptions, null);
    }

    public void JoindreSalle()
    {
        PhotonNetwork.JoinRoom(inputNomSalle.GetComponent<InputField>().text);
    }

    public void ConfirmerNom()
    {
        PhotonNetwork.LocalPlayer.NickName = inputNomJoueur.GetComponent<InputField>().text;
        ecranNom.SetActive(false);
        ecranSalle.SetActive(true);
        print(PhotonNetwork.LocalPlayer.NickName);
    }

   /* public void SelectionJ1() {
        if (ptSelectionnerJ1) {
            j1Selectione = true;
        }
    }

    public void SelectionJ2() {
        if (ptSelectionnerJ2) {
            j2Selectione = true;
        }
    }*/
}
