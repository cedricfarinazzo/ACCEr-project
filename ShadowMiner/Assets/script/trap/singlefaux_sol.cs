using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singlefaux_sol : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerStay(Collider col)
    {
        GameObject jeuobjet = col.gameObject;
        if (jeuobjet.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
