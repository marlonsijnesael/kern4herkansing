using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class Network_UI : MonoBehaviour
{
    public int indexGameScene;

    public float ScrollOffset;
    public float scrollView_minimalHight;

    public Text statusText;
    public GameObject ConnectPannel;

    [Header("Room attributes")]
    public GameObject RoomPannel;
    public Text playercount;

    [Header("Room creation attributes")]
    public GameObject createRoomPannel;
    public InputField textField_RoomName;
    public GameObject scrollViewObject;
    public GameObject scrollViewContent;
    public GameObject searchButton;
    public InputField nicknameField;

    [Header("prefabs")]
    public GameObject buttonPrefab;

    public void ActivateUI(GameObject _UI, bool _activate)
    {
        _UI.SetActive(_activate);
    }

    public void JoinRoom(RoomInfo _rI)
    {
        ActivateUI(scrollViewObject, false);
        PhotonNetwork.JoinRoom(_rI.Name);
    }

    [PunRPC]
    public void LoadScene()
    {
        PhotonNetwork.LoadLevel(indexGameScene);
    Debug.Log("CONNECT!");
  }

    [PunRPC]
    public void SetTextUI(Text _text, string _message)
    {
        _text.text = _message;
    }

    public IEnumerator PopulateScrollView(List<RoomInfo> _roomInfo)
    {
        searchButton.SetActive(false);

        foreach (Transform child in scrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }

        RectTransform scrollViewRect = scrollViewObject.GetComponent<RectTransform>();
        RectTransform scrollViewContentRect = scrollViewContent.GetComponent<RectTransform>();

        Debug.Log("count of rooms: " + PhotonNetwork.CountOfRooms);
        int t = 0;
        int timeOut = 5;
        while (PhotonNetwork.CountOfRooms == 0 && t < timeOut)
        {
            yield return new WaitForSeconds(1);
            t++;
        }

        for (int i = 0; i < PhotonNetwork.CountOfRooms; i++)
        {
            RoomInfo roomInfo = _roomInfo[i];
            GameObject Button = Instantiate(buttonPrefab) as GameObject;
            Button.GetComponent<Button>().onClick.AddListener(delegate { JoinRoom(roomInfo); });

            if (scrollViewObject != null)
            {
                Button.transform.SetParent(scrollViewContent.transform, false);
                Vector3 rectPos = scrollViewRect.transform.position;
                rectPos.y = scrollViewContent.transform.position.y;

                Button.GetComponent<RectTransform>().position = new Vector3(rectPos.x, rectPos.y - ScrollOffset - (ScrollOffset * i), rectPos.z);
                Button.GetComponentInChildren<Text>().text = roomInfo.Name;
            }
        }
        yield return new WaitForEndOfFrame();
        searchButton.SetActive(true);
    }
}
