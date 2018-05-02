using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.MonoBehaviour {

    [SerializeField]
    GameObject player;

    // Use this for initialization
    void Start () {
	    if (PhotonNetwork.isMasterClient)
	    {
		    PhotonNetwork.automaticallySyncScene = false;
	    }
        Instantiate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Instantiate()
    {
        GameObject j = PhotonNetwork.Instantiate(player.name, this.transform.position + Vector3.up * 2, Quaternion.identity, 0);
        j.GetComponent<PlayerController>().enabled = true;
        j.GetComponent<CursorTurnHory>().enabled = true;
        j.GetComponent<Entity>().enabled = true;
        j.GetComponentInChildren<CursorTurnVerti>().enabled = true;
        j.GetComponentInChildren<Camera>().enabled = true;
        j.GetComponentInChildren<AudioListener>().enabled = true;
    }
}
