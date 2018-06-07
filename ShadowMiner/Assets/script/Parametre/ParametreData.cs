using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ParametreData : MonoBehaviour {

    [SerializeField]
    protected Button DeleteButton;

    [SerializeField] protected AudioMixer mixer;

    // Use this for initialization
    void Start () {
        DeleteButton.onClick.AddListener(OnClickDelete);
	}
	
    public void OnClickDelete()
    {
        SaveData.SaveData.DeleteAll();
        SMParametre.Parametre param = SMParametre.Parametre.Load();
        param.Apply();
        param.Save();
        mixer.SetFloat("Main", param.VolumeSonore);
    }
}
