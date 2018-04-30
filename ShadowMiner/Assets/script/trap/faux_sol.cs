using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faux_sol : MonoBehaviour {

	public GameObject fauxsol;

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider col){
		GameObject jeuobjet = col.gameObject;
		if (jeuobjet.tag == "Player") {
			fauxsol.SetActive (false);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
