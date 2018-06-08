using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			if (other.transform.position.y == 15) {
				if (other.transform.position.x < 143) {
					other.transform.position = new Vector3 (-140, 15, other.transform.position.z);
				} else {
					other.transform.position = new Vector3 (-161, 15, other.transform.position.z);
				}
				other.transform.eulerAngles = new Vector3 (other.transform.eulerAngles.x, other.transform.eulerAngles.y+180 >= 360 ? other.transform.eulerAngles.y-180 : 
					other.transform.eulerAngles.y+180, other.transform.eulerAngles.z);
			}
		}
	}
}
