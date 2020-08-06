using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// class contains the network events called to make changes on all clients
/// </summary>
public static class NetworkEvent
{

    /// <summary>
    /// Event called to change the UI on both players
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void UpdateUI(string key, string value)
    {
        object[] data = new object[] { key, value };

        PhotonNetwork.RaiseEvent(
            eventCode: (byte)EventCodes.WebEvents.UPDATE_UI_ELEMENT,
            eventContent: data,
            raiseEventOptions: new RaiseEventOptions { Receivers = ReceiverGroup.All },
            sendOptions: SendOptions.SendUnreliable);
    }

    /// <summary>
    /// Event called to Change the current player's turn 
    /// </summary>
    /// <param name="swapPlayerID"></param>
    public static void ChangePlayerEvent(string currentPlayer)
    {
        object[] data = new object[] { currentPlayer };

        PhotonNetwork.RaiseEvent(
            eventCode: (byte)EventCodes.WebEvents.CHANGE_CURRENT_PLAYER,
            eventContent: data,
            raiseEventOptions: new RaiseEventOptions { Receivers = ReceiverGroup.All },
            sendOptions: SendOptions.SendUnreliable);
    }

    /// <summary>
    /// Event called to Change the current player's turn 
    /// </summary>
    /// <param name="swapPlayerID"></param>
    public static void NextPlayerEvent()
    {
        object[] data = new object[] {  };

        PhotonNetwork.RaiseEvent(
            eventCode: (byte)EventCodes.WebEvents.NEXT_PLAYER,
            eventContent: data,
            raiseEventOptions: new RaiseEventOptions { Receivers = ReceiverGroup.Others },
            sendOptions: SendOptions.SendUnreliable);
    }

    /// <summary>
    /// Event called to update the player gallow on a wrong answer
    /// </summary>
    public static void UpdatePlayerGallows(int index, string player)
    {
        object[] data = new object[] {index, player};

        PhotonNetwork.RaiseEvent(
           eventCode: (byte)EventCodes.WebEvents.UPDATE_GALLOW_SPRITES,
           eventContent: data,
           raiseEventOptions: new RaiseEventOptions { Receivers = ReceiverGroup.All },
           sendOptions: SendOptions.SendUnreliable);
    }

    /// <summary>
    /// test event to see if we could use raise event to send information
    /// </summary>
    public static void ChangeColorOfObject(float r, float g, float b)
    {
        object[] data = new object[] { r, g, b };

        PhotonNetwork.RaiseEvent(
           eventCode: (byte)EventCodes.WebEvents.UPDATE_COLOR,
           eventContent: data,
           raiseEventOptions: new RaiseEventOptions { Receivers = ReceiverGroup.All },
           sendOptions: SendOptions.SendUnreliable);
    }

}
