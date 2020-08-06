using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Linq;

/// <summary>
/// this script keeps the in-game UI up to date
/// </summary>
public class GameUI : MonoBehaviourPun
{
    public static GameUI _Instance;

    public Sprite[] gallowSprites;
    public Image p1Sprite, p2Sprite;
    public List<Text> textsElements = new List<Text>();
    public List<Image> gallows = new List<Image>();
    public Dictionary<string, Text> textObjects = new Dictionary<string, Text>();
    public Dictionary<string, Image> gallowsObjects = new Dictionary<string, Image>();

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        AddElementsToDict();
        p1Sprite.preserveAspect = true;
        p2Sprite.preserveAspect = true;

    }

    //convert UI elements from list to Dict so we can find them by name
    private void AddElementsToDict()
    {
        foreach (Text item in textsElements)
        {
            textObjects.Add(item.name, item);
        }

        foreach (Image item in gallows)
        {
            gallowsObjects.Add(item.name, item);
        }
    }

    //get the right text element from the list
    public Text GetUI(string name)
    {
        return textObjects.Where(obj => obj.Key == name).SingleOrDefault().Value;
    }

    //subscribe to photon event on enable
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    //unsubscribe on disable
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    //Updates the gallow on wrong answer
    //NOTE: the gameover screen shouldn't be implemented here
    //This is a dirty fix in order to be in time for the deadline
    //In the ideal world I would have implemented this inside of the gamemanager class
    private void UpdatePlayerGallow(int value, string obj)
    {
        if (value < gallowSprites.Length)
        {
            if (gallowsObjects.ContainsKey(obj))
            {
                gallowsObjects[obj].sprite = gallowSprites[value];
            }
            else
            {
                Debug.Log("no gallow");
            }
        }
        else
        {
            PhotonNetwork.LoadLevel(3);
        }
    }

    //Pass in the UI element you want to change and its new value
    public void UpdateUI(string key, string value)
    {
        object[] data = new object[] { key, value };
        PhotonNetwork.RaiseEvent((byte)EventCodes.WebEvents.UPDATE_UI_ELEMENT, data, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendUnreliable);
    }

    //Code to run on event received
    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == (byte)EventCodes.WebEvents.UPDATE_UI_ELEMENT)
        {
            object[] data = (object[])obj.CustomData;
            string key = (string)data[0];
            string value = (string)data[1];

            UpdateElement(key, value);
        }

        else if (obj.Code == (byte)EventCodes.WebEvents.CHANGE_CURRENT_PLAYER)
        {
            object[] data = (object[])obj.CustomData;
            //int currentPlayer = (int)data[0];
            string currentPlayer = (string)data[0];
            UpdateElement("current_Player", "Current player: " + currentPlayer);
        }

        else if (obj.Code == (byte)EventCodes.WebEvents.UPDATE_GALLOW_SPRITES)
        {
            object[] data = (object[])obj.CustomData;
            int spriteIndex = (int)data[0];
            string gallowObj = (string)data[1];
            UpdatePlayerGallow(spriteIndex, gallowObj);
        }
    }

    private void UpdateElement(string key, string value)
    {
        if (textObjects.ContainsKey(key))
        {
            Text txt = textObjects[key];
            txt.text = value;
        }
        else
        {
            Debug.LogError(key + " not found");
        }
    }
}

