using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toilettes : MonoBehaviour {

	bool alreadyactivate; //permet d'activer le son une seule fois

	// Use this for initialization
	void Start () {
		alreadyactivate = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerStay(Collider other)
	{
		if (!alreadyactivate && other.gameObject.tag == "Player") {
			AudioSource leson = this.gameObject.GetComponent<AudioSource>();
			leson.Play ();
			alreadyactivate = true;
		}
	}
}
