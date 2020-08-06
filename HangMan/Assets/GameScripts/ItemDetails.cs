using UnityEngine;
using UnityEngine.UI;

public class ItemDetails : MonoBehaviour
{
  public Text text;

  public void SetText(string username, string score)
  {
    text.text = "Player: " + username + " Score: " + score;
  }

}
