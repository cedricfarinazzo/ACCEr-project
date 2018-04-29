using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    [SerializeField]
    protected SceneAsset scene1;
    [SerializeField]
    protected SceneAsset scene2;
    [SerializeField]
    protected SceneAsset scene3;
    [SerializeField]
    protected SceneAsset scene4;

    [SerializeField]
    protected Button but1, but2, but3, but4, exit;

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        exit.onClick.AddListener(EXIT);
        but1.onClick.AddListener(load1);
        but2.onClick.AddListener(load2);
        but3.onClick.AddListener(load3);
        but4.onClick.AddListener(load4);
    }

    void load1()
    {
        LOAD_SCENE(scene1);
    }
    void load2()
    {
        LOAD_SCENE(scene2);
    }
    void load3()
    {
        LOAD_SCENE(scene3);
    }
    void load4()
    {
        LOAD_SCENE(scene4);
    }
    
    public void LOAD_SCENE(SceneAsset scene)
    {
        SceneManager.LoadScene(scene.name);
    }

    public void EXIT()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    
}
