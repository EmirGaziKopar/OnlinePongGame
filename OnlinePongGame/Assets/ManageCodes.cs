using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ManageCodes : MonoBehaviourPunCallbacks
{

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //to connect to the server
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server");

        PhotonNetwork.JoinLobby();

    }


    public override void OnJoinedLobby()
    {
        Debug.Log("Joined to Lobby");

        PhotonNetwork.JoinOrCreateRoom("oda", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined to Room");
        GameObject newPlayer = PhotonNetwork.Instantiate("Rod", Vector3.zero, Quaternion.identity, 0, null);
        newPlayer.GetComponent<PhotonView>().Owner.NickName = Random.Range(1, 100) + "(Misafir)";
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Left to Lobby");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
    }


}
