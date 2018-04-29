using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class choixniveauxsolo : MonoBehaviour {

    [SerializeField]
    public List<Button> niv;
    [SerializeField]
    public List<string> scene;

    //private bool levelfinish = ; // booléen test à supprimer plus tard

	// Use this for initialization
	void Start () {
        for (int i = 0; i < niv.Count && i < scene.Count; i++)
        {
            string name = scene[i];
            niv[i].onClick.AddListener(
            delegate 
            {
                Changementscene(name);
            });
        }
	}

	public void Changementscene(string name)
	{
        SceneManager.LoadScene(name);
	}
			
		
	// Update is called once per frame
	void Update () {
		
	}
}
