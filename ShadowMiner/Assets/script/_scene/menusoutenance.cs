using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menusoutenance : MonoBehaviour {

    [SerializeField]
    protected string scene_cle;
    [SerializeField]
    protected string scene_ant;
    [SerializeField]
    protected string scene_ced;

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    /*
    public void LOAD_SCENE(string name)
    {
        SceneManager.LoadScene(name);
    }
    */

    public void LOAD_SCENE1(){
        SceneManager.LoadScene(this.scene_cle);
	}

	public void LOAD_SCENE2(){
        SceneManager.LoadScene(this.scene_ant);
    }

	public void LOAD_SCENE_MULTI(){
        SceneManager.LoadScene(this.scene_ced);
    }

    public void EXIT()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    
}
