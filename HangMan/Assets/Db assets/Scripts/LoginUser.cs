using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginUser : MonoBehaviour
{
  [SerializeField] private InputField input_Username;
  [SerializeField] private InputField input_Password;
  [SerializeField] private Text statusText;

  public void LogInOnClick()
  {
    StartCoroutine(LoginToDB(input_Username.text, input_Password.text));
  }

  /// <summary>
  /// Coroutine
  /// Use to log in the player on the server
  /// stores the player locally when succesful
  /// </summary>
  private IEnumerator LoginToDB(string username, string password)
  {
    WWWForm form = new WWWForm();
    form.AddField("loginUser", username);
    form.AddField("loginPass", password);

    //POST to login.php on my server
    using (UnityWebRequest www = UnityWebRequest.Post("https://marlonsijnesael.nl/DB/Login.php", form))
    {
      yield return www.SendWebRequest();

      //if there's an error -> show it on the screen
      if (www.isNetworkError || www.isHttpError)
      {
        Debug.LogError(www.error);
        statusText.text = www.error;
      }
      else
      {
        //Login.php returns a json string containing information about the player (username, session token, login succesful, errors)
        //In order to use it we have to deserialize it into a class called DBPlayer
        DBPlayer user = JsonUtility.FromJson<DBPlayer>(www.downloadHandler.text);

        //if there are no errors, we store the player the player client side
        if (MasterManager.instance != null && user.success)
        {
          MasterManager.instance.SetDbPlayer(user);
        }
        else
        {
          statusText.text = user.error;
        }
      }
    }
  }
}
