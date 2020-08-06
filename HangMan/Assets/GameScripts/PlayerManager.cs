using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPun
{
    public NetworkPlayer localPlayer;
    public Photon.Realtime.Player LocalPlayer;
    public int playersNeeded = 2;

    [SerializeField] private int playerCount = 0;
    [SerializeField] private Dictionary<int, NetworkPlayer> playerList = new Dictionary<int, NetworkPlayer>();

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //make sure the masterclient is the first player to register
            PlayerRegistration(0, MasterManager.instance.user.username);            
        }
        else
        {
            photonView.RPC("PlayerRegistration", RpcTarget.MasterClient, 1, MasterManager.instance.user.username);
        }

        NetworkEvent.ChangePlayerEvent(MasterManager.instance.players[Random.Range(0, 1)].nickname);


    }

    [PunRPC]
    private void PlayerRegistration(int count = 0, string nick = "")
    {
        NetworkPlayer newPlayer = new NetworkPlayer(nick);
        newPlayer.playerID = count + 1;
        MasterManager.instance.players[count] = newPlayer;
        localPlayer = newPlayer;
        Debug.Log("player registered: " + newPlayer.playerID);
        playerCount++;
    }
}
