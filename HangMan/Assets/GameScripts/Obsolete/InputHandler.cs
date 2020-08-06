using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputField inputField;

    public string InputChar
    {
        get { return inputField.text; }
        private set { inputField.text = value; }
    }

    public int InputSingleChar { get { return inputField.text.Length; } }

    public void ClearInputField()
    {
        InputChar = "";
    }




}
