using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faux_sol : MonoBehaviour {

    public bool activation = false;
	public GameObject fauxsol;

    public IAtarget sm;

    // Use this for initialization
    void Start () {
		
	}

	void OnTriggerStay(Collider col){
		GameObject jeuobjet = col.gameObject;
		if (activation && jeuobjet.tag == "Player") {
			fauxsol.SetActive (false);
		}
	}
	// Update is called once per frame
	public void Update () {
        activation = sm.targetting;
	}
}
