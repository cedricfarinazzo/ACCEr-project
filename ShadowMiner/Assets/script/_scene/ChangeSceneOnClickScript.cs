using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneOnClickScript : MonoBehaviour {

	public string _nextScene = "";
    public Button butt;

    public void Start()
    {
        butt.onClick.AddListener(Load);
    }

    public void Load()
	{
         SceneManager.LoadScene(_nextScene);
	}
}
