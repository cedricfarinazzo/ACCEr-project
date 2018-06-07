using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParametreSwitchPanel : MonoBehaviour {

    [SerializeField]
    protected Button GraphicsButton;
    [SerializeField]
    protected Button KeyButton;
    [SerializeField]
    protected Button SoundButton;
    [SerializeField]
    protected Button DataButton;
    [SerializeField]
    protected Button BackMenu;

    [SerializeField]
    protected GameObject GraphicsPanel;
    [SerializeField]
    protected GameObject KeyPanel;
    [SerializeField]
    protected GameObject SoundPanel;
    [SerializeField]
    protected GameObject DataPanel;

    // Use this for initialization
    void Start () {
	    Cursor.visible = true;
	    Cursor.lockState = CursorLockMode.None;
        GraphicsButton.onClick.AddListener(ToGraphics);
        KeyButton.onClick.AddListener(ToKey);
        SoundButton.onClick.AddListener(ToSound);
        DataButton.onClick.AddListener(ToData);
        BackMenu.onClick.AddListener(BackToMenu);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToGraphics()
    {
        GraphicsButton.interactable = false;
        KeyButton.interactable = true;
        SoundButton.interactable = true;
        DataButton.interactable = true;
        GraphicsPanel.SetActive(true);
        KeyPanel.SetActive(false);
        SoundPanel.SetActive(false);
        DataPanel.SetActive(false);
    }

    public void ToKey()
    {
        GraphicsButton.interactable = true;
        KeyButton.interactable = false;
        SoundButton.interactable = true;
        DataButton.interactable = true;
        GraphicsPanel.SetActive(false);
        KeyPanel.SetActive(true);
        SoundPanel.SetActive(false);
        DataPanel.SetActive(false);
    }

    public void ToSound()
    {
        GraphicsButton.interactable = true;
        KeyButton.interactable = true;
        SoundButton.interactable = false;
        DataButton.interactable = true;
        GraphicsPanel.SetActive(false);
        KeyPanel.SetActive(false);
        SoundPanel.SetActive(true);
        DataPanel.SetActive(false);
    }

    public void ToData()
    {
        GraphicsButton.interactable = true;
        KeyButton.interactable = true;
        SoundButton.interactable = true;
        DataButton.interactable = false;
        GraphicsPanel.SetActive(false);
        KeyPanel.SetActive(false);
        SoundPanel.SetActive(false);
        DataPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
