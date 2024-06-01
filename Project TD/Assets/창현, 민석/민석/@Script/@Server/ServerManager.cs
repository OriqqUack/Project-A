using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ServerManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Successfully joined the lobby.");
        PhotonNetwork.JoinLobby();
        /*RoomOptions rm = new RoomOptions();
        rm.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("MyRoom", rm, TypedLobby.Default);*/
    }

    public override void OnCreatedRoom()
    {
        //PhotonNetwork.JoinOrCreateRoom("MyRoom", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("I'm connected to the server!!");
        PhotonNetwork.JoinOrCreateRoom("MyRoom", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        //PhotonNetwork.LoadLevel("City");
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print(returnCode + message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print(returnCode + message);
    }
}
