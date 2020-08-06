using UnityEngine;

/// <summary>
/// class to create a scriptable object containing a word, the score per letter and the means to validate any generic word
/// </summary>
[CreateAssetMenu(fileName = "WordAsset", menuName = "HangMan/Words", order = 1)]
[System.Serializable]
public class WordClass : ScriptableObject
{
    public string word = string.Empty;

    private int points = 10;

    public string GetWord { get { return word; } }

    public char[] GetCharArray { get { return word.ToCharArray(); } }

    public int Length { get { return word.Length; } }

    /// <summary>
    /// Function is called from the gamemanager script
    /// It will handle the validation and rewarding/punishment for the given answer
    /// </summary>
    /// <param name="post">text taken from the input field on screen, passed by the gamemanager </param>
    /// <param name="owner">the class representing the player who posted an answer </param>
    public CheckedAnswer HandleSubmission(string post, Gamemanager owner)
    {
        if (ValidateWord(post))
        {
            return OnCharValidated(post, owner);
        }
        else if (ValidateChar(post))
        {
           return  OnCharValidated(post, owner);
        }
      
        return new CheckedAnswer();
    }

    //check if word contains a sigle char
    public bool ValidateChar(string post) => word.Contains(post[0].ToString()) ? true : false;

    //check if the input is equal to the word
    public bool ValidateWord(string post) => word == post ? true : false;

    //callback for when a single char answer is correct
    private CheckedAnswer OnCharValidated(string post, Gamemanager owner)
    {
        //create and return a checkanswer object which contains all the information
        CheckedAnswer result = new CheckedAnswer();
        result.timesInWord = word.Length - word.Replace(post, "").Length;
        result.totalPoints = result.timesInWord * points;
        result.guessedLetters = 1;
        result.correct = true;
        return result;
    }

    //callback for when the whole word is correct
    private CheckedAnswer OnWordValidated(string post, Gamemanager owner)
    {
        CheckedAnswer result = new CheckedAnswer();
        result.totalPoints = word.Length * points;
        result.guessedLetters = word.Length;
        result.correct = true;
        return result;
    }
}

//object to store the results of an answer in
public class CheckedAnswer
{
    public bool correct = false;
    public int guessedLetters = 0;
    public int timesInWord =0;
    public int totalPoints = 0;
}