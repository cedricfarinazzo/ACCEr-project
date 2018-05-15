using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dégats : Trap {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (this.actual_time > 0)
		{
			this.actual_time--;
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Attack(other.gameObject);
		}
	}
}
