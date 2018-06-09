using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Minuteur : Photon.MonoBehaviour {

    [SerializeField] protected int minute;
    [SerializeField] protected int seconde;

    protected int timerout;
    private int starttime;
    private int currenttime;

    public int RestTime
    {
        get { return currenttime; }
    }

	// Use this for initialization
	void Start () {
        timerout = 60 * minute + seconde;
        starttime = (int)Time.time;
        currenttime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        TimeSpan timerrenderer;
        if (0 >= currenttime)
        {
            currenttime = 0;
        }
        timerrenderer = new TimeSpan(0, 0, currenttime);
        
        gameObject.GetComponentInChildren<Text>().text = "Minuteur : " + timerrenderer.ToString();
            
	}

    private void FixedUpdate()
    {
        currenttime = timerout - ((int)Time.time - starttime);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            this.currenttime = (int)stream.ReceiveNext();
        }
        else
        {
            stream.SendNext(currenttime);
        }
    }
}
