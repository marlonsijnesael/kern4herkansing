using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGameManager : MonoBehaviour
{
  public ScoreManager scoreManager;
  public UnityEngine.UI.Text scoreText;



  //pass the playerScore into the database
  private void Awake()
  {
    if (MasterManager.instance != null)
      scoreManager.Set_Score(MasterManager.instance.score);
      scoreText.text = scoreText.text + " " + MasterManager.instance.score.ToString();
  }
}
