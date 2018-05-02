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
        visible = false;
    }

    public void ClickBackMenu()
    {
        SaveData.SaveData.SaveString("niveaux_solo", name);
        SceneManager.LoadScene("loading");
    }
}
