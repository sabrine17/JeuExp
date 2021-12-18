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

    static public bool j1Selectione;
    static public bool j2Selectione;

    public GameObject imgLockJ1;
    public GameObject imgLockJ2;
    public GameObject txtJoueurConnectes;
    public GameObject btnStartNiv1;
    public GameObject btnStartNiv2;


    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;

        j1Selectione = false;
        j2Selectione = false;
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
            btnStartNiv1.SetActive(true);
            btnStartNiv2.SetActive(true);
        }

        else {
            j2Selectione = true;
        }

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
            quitteLobby();
        }

        if (PhotonNetwork.LocalPlayer.IsMasterClient == false && ecranChoixPerso.activeSelf == true)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount < 2) 
            {
                quitteLobby();
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

    void quitteLobby ()
    {
        if (ecranChoixPerso.activeSelf || ecranSalle.activeSelf)
        {
            // PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
            PhotonNetwork.ConnectUsingSettings();
            ecranChoixPerso.SetActive(false);
            ecranSalle.SetActive(false);
            ecranNom.SetActive(false);
        }
    }

    public void partirNiv1 ()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("Niveau1");
        }
    }

    public void partirNiv2()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("Niveau2");
        }
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
