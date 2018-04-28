using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParametreGraphics : MonoBehaviour {

    protected SMParametre.Parametre param;
    [SerializeField]
    protected Button Apply;
    [SerializeField]
    protected Dropdown Quality;
    [SerializeField]
    protected Dropdown Freq;
    [SerializeField]
    protected Dropdown Resolu;
    [SerializeField]
    protected Toggle fullscreen;

	// Use this for initialization
	void Start () {
        param = SMParametre.Parametre.Load();
        param.Apply();
        Freq.value = param.Frequency == 60 ? 1 : 0;
        Quality.value = param.Quality;
        Resolu.value = param.Resolution;
        fullscreen.isOn = param.FullScreen;
        Apply.onClick.AddListener(Save);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Save()
    {
        param.Quality = Quality.value;
        param.Resolution = Resolu.value;
        param.FullScreen = fullscreen.isOn;
        param.Frequency = Freq.value == 0 ? 30 : 60;
        param.Apply();
        param.Save();
    }
}
