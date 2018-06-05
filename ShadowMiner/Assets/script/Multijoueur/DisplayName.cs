using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DisplayName : Photon.MonoBehaviour {

    [NonSerialized]
    private bool enabled = true;
    public static string Name = "Bob";

    // Use this for initialization
    void Start () {
        if (photonView.isMine)
        {
            Name = SaveData.SaveData.GetString("User.login") == "" 
                ? "Bob" : 
                SaveData.SaveData.GetString("User.login");
        }
        UpdateLogin();
	}

    public void UpdateLogin()
    {
        gameObject.GetComponent<TextMesh>().text = Name;
    }

    void OnBecameVisible()
    {
        enabled = true;
    }

    void OnBecameInvisible()
    {
        enabled = false;
    }

    // Update is called once per frame
    void Update () {
        gameObject.SetActive(enabled);
        if (enabled)
        {
            Camera[] list = Camera.allCameras;
            foreach(Camera cam in list)
            {
                Vector3 v = cam.transform.position - transform.position;
                v.x = v.z = 0.0f;
                transform.LookAt(cam.transform.position - v);
            }
        }
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(Name);
        }
        else
        {
            Name = (string)stream.ReceiveNext();
        }
    }
}
