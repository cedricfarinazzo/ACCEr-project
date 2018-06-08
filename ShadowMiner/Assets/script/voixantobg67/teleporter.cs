using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporter : MonoBehaviour {

	public GameObject place_le_teleporter_sortie_ici;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			other.transform.position = new Vector3(other.transform.position.x,other.transform.position.y,place_le_teleporter_sortie_ici.transform.position.z);
			}
		}
	}
