using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class TurnScript : MonoBehaviourPun
{

    public int currentPlayerID = 0;
    public string currentPlayer = "";
    private int swapPlayerID => currentPlayerID == 0 ? 1 : 0;
    private ExitGames.Client.Photon.Hashtable customProperty = new ExitGames.Client.Photon.Hashtable();


    private void Update()
    {
    
    }

    //subscribe to the network event on enable
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
        if (PhotonNetwork.IsMasterClient)
        {
        }
    }

    //unsubscribe to the network event on disable
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    #region events
    //the event called from the masterclient
    //this will change the value on both clients
    private void NetworkingClient_EventReceived(EventData obj)
    {
        Debug.LogFormat("Code: {0}, Sender: {1}, EventName: {2}", obj.Code, obj.Sender, "change player");

        if (obj.Code == (byte)EventCodes.WebEvents.CHANGE_CURRENT_PLAYER)
        {
            object[] data = (object[])obj.CustomData;
            currentPlayer = (string)data[0];
        }
    }



    //use this function to change the player from the gamemanager
    public void ChangePlayer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            currentPlayerID = swapPlayerID;
            NetworkEvent.ChangePlayerEvent(MasterManager.instance.players[currentPlayerID].nickname);
           // SetCustomProperty();
        }
        else
        {
            RequestTurnChange();
        }
    }


    //This function invokes the photon RPC rather than directly calling the function
    private void RequestTurnChange()
    {
        photonView.RPC("RequestTurnChangeRPC", RpcTarget.MasterClient, null);
    }

    //request to raise network event
    //this will ensure that the event is raised from the masterclient
    [PunRPC]
    private void RequestTurnChangeRPC()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            currentPlayerID = swapPlayerID;
            NetworkEvent.ChangePlayerEvent(MasterManager.instance.players[currentPlayerID].nickname);
        }
    }
    #endregion


}
