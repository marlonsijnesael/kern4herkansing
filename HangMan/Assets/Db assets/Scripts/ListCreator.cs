using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ListCreator : MonoBehaviour
{
  [SerializeField]
  private Transform SpawnPoint = null;

  [SerializeField]
  private GameObject item = null;

  [SerializeField]
  private RectTransform content = null;

  [SerializeField]
  private int numberOfItems = 3;


  private void Start()
  {
    StartCoroutine(GetScoresFromDB());
  }

  private IEnumerator GetScoresFromDB()
  {
    using (UnityWebRequest www = UnityWebRequest.Get("https://marlonsijnesael.nl/DB/GetScores.php"))
    {
      yield return www.SendWebRequest();

      if (www.isNetworkError || www.isHttpError)
      {
        Debug.LogError(www.error);
      }
      else
      {
        Debug.Log(www.downloadHandler.text);
        Score[] scores = JsonHelper.getJsonArray<Score>(www.downloadHandler.text);
        foreach(Score s in scores)
        {
          Debug.Log(s.username);
        }
        PopulateList(scores);
   
      }
    }
  }



  void PopulateList(Score[] scores)
  {
    GameObject scoreBox; // Create GameObject instance

    foreach (Score s in scores)
    {
      // Create new instances of our prefab until we've created as many as we specified
      scoreBox = (GameObject)Instantiate(item, transform);
      scoreBox.GetComponent<ItemDetails>().SetText(s.username, s.score.ToString());
    }

  }
}

[System.Serializable]
public class Score
{
  public string username;
  public int score;
  public string date;
}



