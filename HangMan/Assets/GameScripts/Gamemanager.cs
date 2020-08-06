using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// this class handles the game logic
/// </summary>
public class Gamemanager : MonoBehaviourPun
{
    [Header("Functionality")]
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private LetterDisplay letterDisplay;

    public PlayerManager playerManager;
    public TurnHandler turnHandler;
    public Fader fader;

    private string nickName = string.Empty;
    private string scoreUI = "score_Player";
    private string gallowUI = "gallow_Player";

    [Header("Add Words here")]
    [SerializeField] private WordClass[] words;
    [SerializeField] private WordClass currentWord;
    
    public int guessedLetterCount = 0;

    public int GuessedLetterCount
    {
        get { return guessedLetterCount; }
        set { guessedLetterCount = value; }
    }

    private void Awake()
    {
        //register wordclass type as a serializable type and tell photon how to (de)serialize it
        PhotonPeer.RegisterType(typeof(WordClass), (byte)'M', CustomSerializer.Serialize, CustomSerializer.Deserialize);
        nickName = MasterManager.instance.user.username;
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            UpdateWordInGameView();
            scoreUI = "score_Player1";
            gallowUI = "gallow_Player1";
        }
        else
        {
            scoreUI = "score_Player2";
            gallowUI = "gallow_Player2";
        }
    }

    //call the rpc to update the game view
    [PunRPC]
    private void UpdateWordInGameView()
    {
        int randomIndex = Random.Range(0, words.Length);
        currentWord = words[randomIndex];
        photonView.RPC("UpdateOther", RpcTarget.AllBuffered, currentWord);
    }

    //request new word from masterclient
    public void RequestNewWord()
    {
        photonView.RPC("UpdateWordInGameView", RpcTarget.MasterClient);
    }

    //set the new current word on all clients
    [PunRPC]
    public void UpdateOther(WordClass _word)
    {
        currentWord = _word;
        GuessedLetterCount = 0;
        letterDisplay.WordArray = _word.GetCharArray;
        letterDisplay.SetUpDisplay();
    }

    //check the answer and update the screen
    public void CheckAnswer(string input)
    {
        //first we update the display
        letterDisplay.UpdateDisplay(input, currentWord.GetCharArray);
        
        //fetch a checkedanswer from the currentword 
        CheckedAnswer result = currentWord.HandleSubmission(input, this);
        GuessedLetterCount += result.guessedLetters;

        //reward points and update the ui on both clients
        MasterManager.instance.score += result.totalPoints;
        NetworkEvent.UpdateUI(scoreUI, nickName + ": " + MasterManager.instance.score.ToString());

        //if correct show a green flash, if not show a red flash and update the gallow
        if (result.correct)
        {
            fader.FadeRight();
        }
        else
        {
            fader.FadeWrong();
            NetworkEvent.UpdatePlayerGallows(MasterManager.instance.wrongAnswers, gallowUI);
            MasterManager.instance.wrongAnswers++;
        }

        //if the word is guessed, call the rpc to set up a new word
        if (WordGuessed())
        {
            UpdateWordInGameView();
        }
    }

    //if it is my turn:
    //check the input handler 
    //and submit/checkt the input
    public void SubmitInput()
    {
        if (turnHandler.isMyTurn)
        {
            string input = WordExtensions.CleanInput(inputHandler.InputChar);
            if (input != string.Empty && CheckIfInputIsValid(input))
            {
                CheckAnswer(input);
                turnHandler.StopTurnOnSubmit();
                inputHandler.ClearInputField();
            }
        }
    }

    //validate the current user's input
    private bool CheckIfInputIsValid(string input)
    {
        return !letterDisplay.currentText.Contains(input) || input.Length == currentWord.Length;
    }

    private bool WordGuessed()
    {
        return letterDisplay.currentText == currentWord.word;
    }
}
