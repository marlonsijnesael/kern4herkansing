using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour
{

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Alpha5))
      UpdateScore();
  }

  private void UpdateScore()
  {
    Set_Score();
  }


  [ContextMenu("force score")]
  public void Set_Score(int score = 0)
  {
    StartCoroutine("SetScoreOnDB", score);
  }
 

  private IEnumerator SetScoreOnDB(int score)
  {
    WWWForm form = new WWWForm();
    form.AddField("session_id", MasterManager.instance.user.session);
    form.AddField("score", score);
    using (UnityWebRequest www = UnityWebRequest.Post("https://marlonsijnesael.nl/DB/SetScore.php", form))
    {

      yield return www.SendWebRequest();

      if (string.IsNullOrEmpty(www.error))
      {
        print(www.downloadHandler.text);
      }
      else
      {
        Debug.LogError(www.error);
      }
    }
  }
}
