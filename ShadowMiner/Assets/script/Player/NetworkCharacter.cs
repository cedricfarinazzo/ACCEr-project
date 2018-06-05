using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCharacter : Photon.MonoBehaviour {

    Vector3 trueLoc;
    Quaternion trueRot;
    PhotonView pv;

    [SerializeField]
    private List<Vector3> posList;

    private bool onGame = false;

    private int _photonId;
    
    // Use this for initialization
    void Start () {
        this.pv = GetComponent<PhotonView>();
        _photonId = PhotonNetwork.room.PlayerCount - 1;
    }
	
	// Update is called once per frame
	void Update () {
	    if (!pv.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, trueLoc, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, trueRot, Time.deltaTime);
            
        }
        gameObject.name = this.GetComponent<PhotonView>().owner.name;
        Debug.Log(PhotonNetwork.room.PlayerCount);
	    if (PhotonNetwork.room.PlayerCount == 3 && !onGame)
	    {
	        onGame = true;
	        MoveToGame();
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

    void MoveToGame()
    {
        Debug.Log("MoveToGame");
        this.gameObject.transform.position = posList[_photonId];
    }
}
