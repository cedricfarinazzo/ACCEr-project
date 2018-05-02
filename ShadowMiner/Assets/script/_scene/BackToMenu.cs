using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour {

    [SerializeField]
    public string menu;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (PhotonNetwork.inRoom)
            {
                PhotonNetwork.LeaveRoom();
            }

            if (PhotonNetwork.insideLobby)
            {
                PhotonNetwork.LeaveRoom();
            }

            if (PhotonNetwork.connected)
            {
                PhotonNetwork.Disconnect();
            }
            SaveData.SaveData.SaveString("menu", name);
            SceneManager.LoadScene("loading");
        }
    }
}
