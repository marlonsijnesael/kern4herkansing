using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LetterDisplay : MonoBehaviourPun
{
    [SerializeField] private Text uI_Text;

    public string currentText { get { return uI_Text.text; } }

    private char[] defaultText;
    private char[] wordArray;

    public char[] WordArray { get { return wordArray; } set { wordArray = value; } }

    public void UpdateDisplay(string _post, char[] _currentWord)
    {
        string newText = string.Empty;

        if (_post == "")
        {
            return;
        }

        if (_post == new string(_currentWord))
        {
            newText = _post;
        }

        else
        {
            char[] chars = uI_Text.text.ToCharArray();
            char post = _post.ToCharArray()[0];

            for (int i = 0; i < chars.Length; i++)
            {
                if (_currentWord[i] == post)
                {
                    chars[i] = post;
                }
            }

            newText = new string(chars);
            uI_Text.text = newText;
        }

        //GameUI._Instance.UpdateUI("word UI", newText);
        NetworkEvent.UpdateUI("word UI", newText);

    }

    public void SetUpDisplay()
    {
        string newText = string.Empty;
        for (int c = 0; c < wordArray.Length; c++)
        {
            newText += "*";
        }
        uI_Text.text = newText;

        //GameUI._Instance.UpdateUI("word UI", newText);
        NetworkEvent.UpdateUI("word UI", newText);
    }



}
