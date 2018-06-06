using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayName : MonoBehaviour {

    private bool enabled = true;

    // Use this for initialization
    void Start () {

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
        gameObject.GetComponent<MeshRenderer>().enabled = enabled;
        if (enabled)
        {
            Camera[] list = Camera.allCameras;
            foreach(Camera cam in list)
            {
                Vector3 v = cam.transform.position - transform.position;
                v.x = v.z = 0.0f;
                transform.LookAt(cam.transform.position - v);
                transform.Rotate(0, 180, 0);
            }
        }
	}
}
