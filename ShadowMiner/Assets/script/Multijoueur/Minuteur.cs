using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using SMProgress;
using SMNetwork.Client;

public class Minuteur : Photon.MonoBehaviour {

    [SerializeField] protected int minute;
    [SerializeField] protected int seconde;

    [SerializeField] protected Text timer;

    [SerializeField] protected GameObject EndCanvas, Minerwon, Minerloose, smwon, smloose;

    protected int timerout;
    private int starttime;
    private int currenttime;

    private bool end = false;

    public int RestTime
    {
        get { return currenttime; }
    }

	// Use this for initialization
	void Start () {
        timer.GetComponent<Text>().enabled = true;
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
            EndTime();
        }else
        {
            PlayerIsDead();
        }
        timerrenderer = new TimeSpan(0, 0, currenttime);
        
        timer.text = "Timer : " + timerrenderer.ToString();

        
            
	}

    public void PlayerIsDead()
    {
        if (transform.parent.GetComponentInChildren<LobbyManager>().PhotonPlayer == null)
        {
            GameObject.Find("ThirdCamera").GetComponentInChildren<Camera>().enabled= true;
        }
    }

    void EndTime()
    {
        if (end)
        {
            return;
        }
        end = true;

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject player = transform.parent.GetComponentInChildren<LobbyManager>().PhotonPlayer;
        EndCanvas.SetActive(true);
        if (player == null)
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
            {
                SMWon(false);
            }
            else
            {
                MinerWon(false);
            }
            
        }
        else if (player.tag == "Monster")
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
            {
                SMWon(true);
            }
            else
            {
                MinerWon(true);
            }
        }
        else
        {
            MinerWon(false);
        }
    }

    private void MinerWon(bool issm)
    {
        if (issm)
        {
            smloose.SetActive(true);
        }
        else
        {
            Minerwon.SetActive(true);
            SMProgress.Progress.IncrementMulti();
            Client SMClient = new Client();
            Progress progress = Progress.Load();
            SMClient.UpdateProgress(progress.SoloStats, progress.MultiStats);
        }
    }

    private void SMWon(bool issm)
    {
        if (issm)
        {
            smwon.SetActive(true);
            SMProgress.Progress.IncrementMulti();
            Client SMClient = new Client();
            Progress progress = Progress.Load();
            SMClient.UpdateProgress(progress.SoloStats, progress.MultiStats);
        }
        else
        {
            Minerloose.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        currenttime = timerout - ((int)Time.time - starttime);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(currenttime);
        }
        else
        {
            int receive = (int)stream.ReceiveNext();
            if (receive > currenttime)
            {
                currenttime = receive;
            }
        }
    }
}
