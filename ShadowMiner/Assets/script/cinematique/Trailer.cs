﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Trailer : MonoBehaviour {

    public VideoPlayer player;

	// Use this for initialization
	void Start () {
        player.loopPointReached += OnEnd;
	}
	
	public void OnEnd(VideoPlayer vp)
    {
        SaveData.SaveData.SaveString("Loader.Next", "menu");
        SceneManager.LoadScene("loading");
    }
}