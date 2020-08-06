using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using ExitGames.Client.Photon;

public class TurnHandler : MonoBehaviour
{
    public bool isMyTurn = false;

    private float elapsedTime = 0;
    private bool timerRunning = false;

    [SerializeField] private float time = 30f;
    [SerializeField] private int turn = 0;
    [SerializeField] private int turnsThisGame = 0;
    [SerializeField] private Text timerText;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            OnStartTurn();
        }
    }

    //subscribe to the network events
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    //unsubscribe to the network events
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        Debug.LogFormat("Code: {0}, Sender: {1}, EventName: {2}", obj.Code, obj.Sender, "changed players");

        if (obj.Code == (byte)EventCodes.WebEvents.NEXT_PLAYER)
        {
            OnStartTurn();
        }
    }

    private void CheckTurn()
    {
        if (turn >= turnsThisGame)
        {
            PhotonNetwork.LoadLevel(3);
        }
    }

    //reset turn variables and start timer
    private void OnStartTurn()
    {
        turn++;
        isMyTurn = true;
        elapsedTime = 0;
        timerRunning = true;
        timerText.color = Color.green;
        Coroutine timer = StartCoroutine(Timer());
    }

    //end turn and set the next player
    private void OnEndTurn()
    {
        if (PhotonNetwork.IsMasterClient)
            CheckTurn();

        NetworkEvent.NextPlayerEvent();
        timerText.text = "Waiting for other player..";
        isMyTurn = false;
    }

    //stop turn when player submits an answer
    public void StopTurnOnSubmit()
    {
        timerRunning = false;
        timerText.color = Color.black;
    }

    //keeps track of the time in turn, ends it when timer <= zero
    private IEnumerator Timer()
    {
        while (elapsedTime < time && timerRunning)
        {
            elapsedTime += Time.deltaTime;
            int timeDisplay = (int)(time - elapsedTime);
            timerText.text = timeDisplay.ToString();
            if (timeDisplay < 10)
            {
                timerText.color = Color.red;
            }
            yield return new WaitForEndOfFrame();
        }
        OnEndTurn();
    }

    
}
