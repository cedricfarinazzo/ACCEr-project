using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackMenu : MonoBehaviour {

    [SerializeField]
    protected Button Back;

    // Use this for initialization
    void Start () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Back.onClick.AddListener(BackToMenu);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
