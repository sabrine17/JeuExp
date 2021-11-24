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

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connecté " + PhotonNetwork.IsConnected);
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("le joueur a déconnecté " + cause.ToString());
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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
