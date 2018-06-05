using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayName : Photon.MonoBehaviour {

    private bool enabled;

	// Use this for initialization
	void Start () {
        if (photonView.isMine)
        {
            string login = SaveData.SaveData.GetString("User.login") == "" 
                ? "Bob" : 
                SaveData.SaveData.GetString("User.login");
            gameObject.GetComponent<TextMesh>().text = login;
        }
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
}
