using System.Collections;
using System.Collections.Generic;
using SMParametre;
using UnityEngine;
using UnityEngine.Audio;

public class UpdateVolume : Photon.MonoBehaviour
{


    [SerializeField] protected AudioMixer mixer;

    private void Start()
    {
        if (PhotonNetwork.connectedAndReady)
        {
            if (photonView.isMine)
            {
                SMParametre.Parametre p = Parametre.Load();
                mixer.SetFloat("Main", p.VolumeSonore);
            }
        }
        else
        {
            SMParametre.Parametre p = Parametre.Load();
            mixer.SetFloat("Main", p.VolumeSonore);
        }
    }
}
