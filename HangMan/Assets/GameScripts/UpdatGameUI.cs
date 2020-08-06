using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UIUpdate", menuName = "HangMan/Events/UI", order = 1)]
[System.Serializable]
public class UpdatGameUI : BaseEvent
{
    [SerializeField] private Image playerOneSprite;
    [SerializeField] private Image playerTwoSprite;
    [SerializeField] private Sprite[] spriteSheet;

    private int spriteIndexPlayerOne = 0;
    private int spriteIndexPlayerTwo = 0;

    // Start is called before the first frame update
    public override void NetworkingClient_EventReceived(EventData obj)
    {
        base.NetworkingClient_EventReceived(obj);
    }

    public override void RaiseEvent()
    {
        base.RaiseEvent();
    }

}
