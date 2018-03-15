using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCharacter : Photon.MonoBehaviour {

    Vector3 trueLoc;
    Quaternion trueRot;
    PhotonView pv;

    // Use this for initialization
    void Start () {
        this.pv = GetComponent<PhotonView>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!pv.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, trueLoc, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, trueRot, Time.deltaTime);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isReading)
        {
            if (!pv.isMine)
            {
                this.trueLoc = (Vector3)stream.ReceiveNext();
            }
        }
        else
        {
            if (pv.isMine)
            {
                stream.SendNext(transform.position);
            }
        }
    }
}
