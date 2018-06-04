using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    [SerializeField]
    protected string scene1;
    [SerializeField]
    protected string scene2;
    [SerializeField]
    protected string scene3;
    [SerializeField]
    protected string scene4;
    [SerializeField]
    protected string scene5;
    [SerializeField]
    protected string scene6;

    [SerializeField]
    protected Button but1, but2, but3, but4, but5, but6, exit, website;

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        exit.onClick.AddListener(EXIT);
        but1.onClick.AddListener(load1);
        but2.onClick.AddListener(load2);
        but3.onClick.AddListener(load3);
        but4.onClick.AddListener(load4);
        but5.onClick.AddListener(load5);
        but6.onClick.AddListener(load6);
        website.onClick.AddListener(OPEN_WEBSITE);
    }

    void load1()
    {
        SaveData.SaveData.SaveString("Loader.Next", scene1);
        SceneManager.LoadScene("loading");
    }
    void load2()
    {
        SaveData.SaveData.SaveString("Loader.Next", scene2);
        SceneManager.LoadScene("loading");
        //LOAD_SCENE(scene2);
    }
    void load3()
    {
        SaveData.SaveData.SaveString("Loader.Next", scene3);
        SceneManager.LoadScene("loading");
    }
    void load4()
    {
        LOAD_SCENE(scene4);
    }

    void load5()
    {
        SaveData.SaveData.SaveString("Loader.Next", scene5);
        SceneManager.LoadScene("loading");
    }

    void load6()
    {
        SaveData.SaveData.SaveString("Loader.Next", scene6);
        SceneManager.LoadScene("loading");
    }

    public void LOAD_SCENE(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void EXIT()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    
    public void OPEN_WEBSITE()
    {
        Application.OpenURL("https://accer.ddns.net/");
    }
    
}
