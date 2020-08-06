using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Functionallity")]
    [SerializeField] private Network_UI UI;

    [Header("Settings")]
    [SerializeField] private int minimumPlayers = 0;
    [SerializeField] private readonly string gameVersion = "1";

    [Header("Prefabs")]
    [SerializeField] private GameObject buttonPrefab;

    private int playerCount = 0;
    private List<RoomInfo> roomList = new List<RoomInfo>();

    #region Monobehaviour callbacks
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        UI.ActivateUI(UI.ConnectPannel, true);
    }

    private void Start()
    {
    }
    #endregion

    #region PUN overrides    
    //callback for when basic connectection to master is established
    public override void OnConnectedToMaster()
    {
        UI.SetTextUI(UI.statusText, "connected to master. Welcome:  " + MasterManager.instance.user);
        PhotonNetwork.JoinLobby();
    }

    //callback when player is succesfully connected to a lobby
    public override void OnJoinedLobby()
    {
        UI.SetTextUI(UI.statusText, "joined lobby");
        if (PhotonNetwork.CountOfRooms == 0)
        {
            UI.SetTextUI(UI.statusText, "Welcome player: " + MasterManager.instance.user.username);
            UI.ActivateUI(UI.createRoomPannel, true);
        }
        else
        {
            UI.ActivateUI(UI.scrollViewObject, true);
        }
    }

    //callback when player has succesfully joined or created a room
    public override void OnJoinedRoom()
    {
        UI.SetTextUI(UI.statusText, "joined room: " + PhotonNetwork.CurrentRoom.Name);
        UI.ActivateUI(UI.RoomPannel, true);
        RegisterPlayer();
        photonView.RPC("UpdatePlayers", RpcTarget.All);
    }

    //callback when a room is created, only recieved while in a lobby
    public override void OnRoomListUpdate(List<RoomInfo> _roomList)
    {
        foreach (RoomInfo rI in _roomList)
        {
            roomList.Add(rI);
        }
    }
    #endregion

    #region Networking functions

    //connect to photon lobby
    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
            UI.SetTextUI(UI.statusText, "establishing connection..");
        }
    }

    /// <summary>
    ///Refresh the room list by disconnected en reconnecting to the photon lobby
    ///since photon lobby does not allow for sending npc´s to people who are not in a room
    /// </summary>
    public void UpdateRoomList()
    {
        UI.ActivateUI(UI.scrollViewObject, true);
        Coroutine ListUpdate = StartCoroutine(UI.PopulateScrollView(roomList));
    }

    //checks if the room exists or if it should be created
    public void CreateRoom()
    {
        UI.SetTextUI(UI.statusText, "creating room");
        string roomName = UI.textField_RoomName.text;

        if (roomName == "" || CheckIfRoomExist(roomName))
        {
            roomName = MasterManager.instance.user.username + "'s room";
        }

        PhotonNetwork.CreateRoom(roomName);
        UI.SetTextUI(UI.playercount, "waiting for player to join");
    }

    //check if there is already a room with the given name
    private bool CheckIfRoomExist(string _name)
    {
        foreach (RoomInfo rI in roomList)
        {
            if (rI.Name == _name)
            {
                return true;
            }
        }
        return false;
    }
    
    //register player on both clients
    private void RegisterPlayer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //make sure the masterclient is the first player to register
            PlayerRegistration(0, MasterManager.instance.user.username);
        }
        else
        {   //set the opponent's client to player 2 -> because the "it's my house, so I get the better controller" - law
            photonView.RPC("PlayerRegistration", RpcTarget.MasterClient, 1, MasterManager.instance.user.username);
        }
    }
    #endregion

    #region RPCs
    //register the new player on both the clients' mastermanager instance
    [PunRPC]
    private void PlayerRegistration(int count = 0, string nick = "")
    {
        NetworkPlayer newPlayer = new NetworkPlayer(nick);
        newPlayer.lives = 11;
        newPlayer.isAlive = true;
        MasterManager.instance.players[count] = newPlayer;
        MasterManager.instance.localPlayer = newPlayer;
        Debug.Log("player registered: " + newPlayer.playerID);
    }

    [PunRPC]
    public void UpdatePlayers()
    {
        playerCount++;
        if (playerCount == minimumPlayers)
        {
            photonView.RPC("LoadScene", RpcTarget.All);
        }
    }
    #endregion
}
