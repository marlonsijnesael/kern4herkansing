using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegisterUser : MonoBehaviour
{
  [SerializeField] private ValidateInput input_Email;
  [SerializeField] private ValidateInput input_Username;
  [SerializeField] private ValidateInput input_Password;

  [SerializeField] private Text statusText;

  public void RegisterOnClick()
  {
    if (input_Email.isValid && input_Username.isValid && input_Password.isValid)
    {
      string username = input_Username.inputField.text;
      string email = input_Email.inputField.text;
      string password = input_Password.inputField.text;

      StartCoroutine(RegisterUserToDB(email, username, password));
    }
  }

  /// <summary>
  /// Coroutine
  /// Use to register the player on the server
  /// stores the player locally when succesful
  /// </summary>
  private IEnumerator RegisterUserToDB(string email, string username, string password)
  {
    WWWForm form = new WWWForm();
    form.AddField("loginEmail", email);
    form.AddField("loginUser", username);
    form.AddField("loginPass", password);

    //POST to RegisterUser.php on my server
    using (UnityWebRequest www = UnityWebRequest.Post("https://marlonsijnesael.nl/DB/RegisterUser.php", form))
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
        //RegisterUser.php returns a json string containing information about the player (username, session token, login succesful, errors)
        //In order to use it we have to deserialize it into a class called DBPlayer
        DBPlayer user = JsonUtility.FromJson<DBPlayer>(www.downloadHandler.text);
        if (user.success)
        {
          statusText.text = "SUCCES!";

          if (MasterManager.instance != null)
          {
            MasterManager.instance.SetDbPlayer(user);
          }
        }
        else
        {
          statusText.text = user.error;
          input_Username.inputField.textComponent.color = Color.red;
        }
      }
    }
  }
}
