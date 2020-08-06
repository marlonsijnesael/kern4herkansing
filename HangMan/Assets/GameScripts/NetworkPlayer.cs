using UnityEngine;

[System.Serializable]
public class NetworkPlayer
{
    [SerializeField] public int score = 0;
    [SerializeField] public string nickname = "";
    [SerializeField] public int playerID = 0;
    public int lives = 11;
    public int wrongAnswers;
    public bool isAlive = true;

    public NetworkPlayer(string _nickname)
    {
        nickname = _nickname;
        //playerID = _playerID;
    }

    public int GetScore { get { return score; } }

    public void punish(int amount)
    {
        wrongAnswers += amount;

        if (lives - wrongAnswers <= 0)
        {
            isAlive = false;
        }
    }

    public void ChangeScore(int _modifier)
    {
        score += _modifier;
        Debug.Log("new score: " + score);
    }
}
