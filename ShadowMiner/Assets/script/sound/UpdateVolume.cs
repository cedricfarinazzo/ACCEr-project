using System.Collections;
using System.Collections.Generic;
using SMParametre;
using UnityEngine;
using UnityEngine.Audio;

public class UpdateVolume : MonoBehaviour
{
    [SerializeField] protected AudioMixer mixer;

    private void Start()
    {
        SMParametre.Parametre p = Parametre.Load();
        mixer.SetFloat("Main", p.VolumeSonore);
    }
}
