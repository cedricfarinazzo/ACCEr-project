using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

    protected SMParametre.Parametre param;

    [SerializeField]
    protected string Next;

    // Use this for initialization
    void Start () {
        param = SMParametre.Parametre.Load();
        param.Apply();
        StartCoroutine(LoadYourAsyncScene());
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Next);

        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }


}
