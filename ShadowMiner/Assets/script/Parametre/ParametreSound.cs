using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ParametreSound : MonoBehaviour {

    protected SMParametre.Parametre param;
    [SerializeField]
    protected Slider Sound;

    [SerializeField] protected AudioMixer mixer;

	// Use this for initialization
	void Start () {
        param = SMParametre.Parametre.Load();
        param.Apply();
        Sound.value = param.VolumeSonore;
        Sound.onValueChanged.AddListener(Change);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Change(float arg0)
    {
        param.VolumeSonore = Sound.value;
        mixer.SetFloat("Main", param.VolumeSonore);
        param.Save();
    }
}
