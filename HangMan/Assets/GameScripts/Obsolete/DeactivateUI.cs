using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateUI : MonoBehaviourPun
{
    ////photon event settings: Change current player
    //private byte eventCode = EventCodes.CHANGE_CURRENT_PLAYER;

    //private string eventName = "test";

    //public void OnEnable()
    //{
    //    PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    //}

    //public void OnDisable()
    //{
    //    PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    //}

    //private void NetworkingClient_EventReceived(EventData obj)
    //{
    //    Debug.LogFormat("Code: {0}, Sender: {1}, EventName: {2}", obj.Code, obj.Sender, eventName);
    //    if (obj.Code == eventCode)
    //    {
    //        object[] data = (object[])obj.CustomData;
    //        Debug.Log("syke");
    //    }
    //}



    //public void RaiseEvent()
    //{
        
    //    object[] data = new object[] { "oo" };

    //    PhotonNetwork.RaiseEvent(eventCode, data, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    //}


}


