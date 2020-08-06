using UnityEngine;
using System.Text;
using System;

/// <summary>
/// Custom serialization class for the word class
/// Photon doesn't support custom types in RPCs without
/// giving it a proper way to serialize and deserialize it
/// The custom functions are passed into photon in the networkmanager
/// </summary>

[System.Serializable]
public class CustomSerializer : MonoBehaviour
{
    public static byte[] Serialize(object obj)
    {
        WordClass data = (WordClass)obj;

        byte[] myStringBytes = Encoding.ASCII.GetBytes(data.word);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(myStringBytes);

        return myStringBytes;
    }

    public static object Deserialize(byte[] bytes)
    {
        WordClass data = new WordClass();

        if (bytes.Length > 0)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            data.word = Encoding.UTF8.GetString(bytes);
            Debug.Log(data.word);
        }
        else
        {
            data.word = "not working";
        }

        return data;
    }
}
