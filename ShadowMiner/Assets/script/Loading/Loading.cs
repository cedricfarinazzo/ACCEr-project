using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

    protected SMParametre.Parametre param;

    [SerializeField]
    public string NextDefault;

    [SerializeField] protected Image bar;
    [SerializeField] protected Text text;

    private string Next;
    
    // Use this for initialization
    void Start () {
        //Application.LoadLevel(1);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        string newnext = SaveData.SaveData.GetString("Loader.Next");
        if (newnext == "")
        {
            Next = NextDefault;
        }
        else
        {
            Next = newnext;
        }
        SaveData.SaveData.DeleteKey("Loader.Next");
        if (newnext == "")
        {
            param = SMParametre.Parametre.Load();
            param.Apply();
        }
        StartCoroutine(LoadYourAsyncScene());
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.

        AsyncOperation asyncLoad;

        try
        {
            asyncLoad= SceneManager.LoadSceneAsync(Next);
        }
        catch (Exception e)
        {
            asyncLoad= SceneManager.LoadSceneAsync("menu");
        }
           
        
        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("Progress: "+progress.ToString());
            bar.fillAmount = progress;
            text.text = "Loading ...             " + (progress * 100) + "%";
            yield return null;
        }
    }
}
