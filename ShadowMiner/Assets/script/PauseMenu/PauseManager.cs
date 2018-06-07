using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    [SerializeField] protected GameObject canvas;
    [SerializeField] protected Button Resume;
    [SerializeField] protected Button BackMenu;

    [SerializeField]
    private bool visible = false;

    private KeyCode key = KeyCode.Escape;
    
    private void Start()
    {
        key = SMParametre.Parametre.Load().Key["Escape"];
    }

    private void Update()
    {
        /*
        if (visible && Input.GetKey(key))
        {
            ClickResume();
        }*/
        if (Input.GetKeyDown(key))
        {
            visible = !visible;
        }

        if (visible)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            canvas.SetActive(true);
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            canvas.SetActive(false);
        }
    }

    public void ClickResume()
    {
        Debug.Log("Resume");
        visible = false;
    }

    public void ClickBackMenu()
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
        SaveData.SaveData.SaveString("Loader.Next", "menu");
        SceneManager.LoadScene("loading");
    }
}
