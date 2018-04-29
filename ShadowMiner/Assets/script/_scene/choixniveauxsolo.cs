using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class choixniveauxsolo : MonoBehaviour {
    /*
    public Button niv1;
	public Button niv2;
	public Button niv3;
	public Button niv4;
	public Button niv5;
	public Button niv6;
	public Button niv7;
	public Button niv8;
	public Button niv9;
	public Button niv10;
	public Button niv11;
	public Button niv12;
	public Button niv13;
	public Button niv14;
	public Button niv15;
	public Button niv16;
	public Button niv17;
	public Button niv18;
	public Button niv19;
	public Button niv20;
    */
    [SerializeField]
    public List<Button> niv;
    [SerializeField]
    public List<SceneAsset> scene;

    //private bool levelfinish = ; // booléen test à supprimer plus tard

	// Use this for initialization
	void Start () {
        for (int i = 0; i < niv.Count; i++)
        {
            niv[i].onClick.AddListener(
            delegate 
            {
                Changementscene(i);
            });
        }
	}

	public void Changementscene(int i)
	{
        SceneManager.LoadScene(scene[i].name);
	}
			
		
	// Update is called once per frame
	void Update () {
		
	}
}
