using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;


public class BaseEvent : ScriptableObject
{
    //photon event settings: Change current player
    public byte eventCode;
    public RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
    public SendOptions sendOptions = new SendOptions { Reliability = false };
    public string eventName = "updateUI";

    public virtual void NetworkingClient_EventReceived(EventData obj)
    {
        Debug.LogFormat("Code: {0}, Sender: {1}, EventName: {2}", obj.Code, obj.Sender, eventName);
       
    }

    public virtual void RaiseEvent()
    {
    }

}


