using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// persistent manager object (singleton)
/// keeps track of the player's score, user information and the other players
/// </summary>
public class MasterManager : MonoBehaviour
{
    public static MasterManager instance;
    public int score;
    public int wrongAnswers;

    [Header("players")]
    public DBPlayer user;

    [Header("list of registered players in room")]
    public NetworkPlayer[] players = new NetworkPlayer[2];
    public NetworkPlayer localPlayer;

    //create singleton
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    //check if the player is connected to the internet 
    public bool IsConnectedToInternet
    {
        get { return Application.internetReachability != NetworkReachability.NotReachable; }
    }

    private void Update()
    {
        //if we are in lobby/game and lose internetconnection -> return to lobby
        if (!IsConnectedToInternet && SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
        {
            LogOut();
        }
    if (Input.GetKeyDown(KeyCode.Alpha8))
    {
      SceneManager.LoadScene("EndScene");
    }
    }

    //tell the server to register the current score
    public void Set_Score()
    {
        StartCoroutine("SetScoreOnServer", score);
    }

    //make the userinfo persistent inside of the singleton
    public void SetDbPlayer(DBPlayer user)
    {
        this.user = user;
    SceneManager.LoadScene("Lobby");
    }

    //log out and return to login screen 
    //also destroy this object in order to make room for a new singleton instance
    //because this one does not have its references anymore
    public void LogOut()
    {
        user = null;
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }
}
