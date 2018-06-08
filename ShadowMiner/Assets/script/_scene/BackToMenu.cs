using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMenu : MonoBehaviour {

    [SerializeField]
    public string menu;

    [SerializeField]
    public GameObject success;

    public void Start()
    {
        success.GetComponentInChildren<Button>().onClick.AddListener(ClickToMenu);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
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
            Time.timeScale = 0f;
            string scenename = SceneManager.GetActiveScene().name;
            if (scenename.StartsWith("Solo_level_"))
            {
                string[] scene = scenename.Split('_');
                int sceneid = int.Parse(scene[scene.Length - 1]);
                SMProgress.Progress.IncrementSolo(sceneid);
                success.GetComponentInChildren<Text>().text = "You have survived at the level " + sceneid + "!";
            }
            other.gameObject.GetComponentInChildren<PauseManager>().enabled = false;
            success.SetActive(true);
        }
    }

    public void ClickToMenu()
    {
        SaveData.SaveData.SaveString("Loader.Next", menu);
        Time.timeScale = 1f;
        SceneManager.LoadScene("loading");
    }
}
