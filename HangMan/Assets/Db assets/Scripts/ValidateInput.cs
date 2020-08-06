using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

/// <summary>
/// Collection of Regex string validations
/// Used to validate the imput clientside before sending it to the server
/// </summary>
public class ValidateInput : MonoBehaviour
{
  public enum InputType { USERNAME = 0, EMAIL = 1, PASSWORD = 2 };
  public InputType inputType = new InputType();
  public InputField inputField;
  public Text errorText;
  public Color errorColor;
  public string errorMessage;

  public bool isValid = false;

  private void Start()
  {
    //first we check what kind of validation is required for this field
    if (inputType == InputType.USERNAME)
    {
      inputField.onValueChanged.AddListener(delegate { ValidateUsername(); });
    }
    else if (inputType == InputType.EMAIL)
    {
      inputField.onValueChanged.AddListener(delegate { ValidateEmail(); });
    }
    else
    {
      inputField.onValueChanged.AddListener(delegate { ValidatePassword(); });
    }
  }

  public void ValidatePassword()
  {
    string password = inputField.text;
    Regex hasNumber = new Regex(@"[0-9]+");
    Regex hasUpperChar = new Regex(@"[A-Z]+");
    Regex hasMinimum8Chars = new Regex(@".{8,}");

    isValid = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);

    ShowValid();
  }

  public void ValidateEmail()
  {
    string email = inputField.text;
    Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                            + "@"
                            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
    isValid = regex.Match(email).Success;

    ShowValid();
  }

  public void ValidateUsername()
  {
    string username = inputField.text;
    Regex regex = new Regex("^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$");
    isValid = regex.Match(username).Success;
    ShowValid();

  }

  private void ShowValid()
  {
    if (!isValid)
    {
      errorText.color = errorColor;
      errorText.text = errorMessage;
    }
    else
    {
      errorText.color = Color.black;
      errorText.text = "";
    }
  }
}




/* ^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$
 └─────┬────┘└───┬──┘└─────┬─────┘└─────┬─────┘ └───┬───┘
       │         │         │            │           no _ or . at the end
       │         │         │            │
       │         │         │            allowed characters
       │         │         │
       │         │         no __ or _. or ._ or .. inside
       │         │
       │         no _ or . at the beginning
       │
       username is 8-20 characters long

    */
