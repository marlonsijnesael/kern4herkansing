using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;

public class WordExtensions : MonoBehaviour
{
    public static string CleanInput(string strIn)
    {
        strIn.ToLower();
        // Replace invalid characters with empty strings.
        try
        {
            return Regex.Replace(strIn, @"[^\w\.@-]", "", RegexOptions.None, System.TimeSpan.FromSeconds(1.5f));
        }
        // If we timeout when replacing invalid characters, 
        // we should return Empty.
        catch (RegexMatchTimeoutException)
        {
            return string.Empty;
        }
    }
}
