using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_etagere : MonoBehaviour {

	private KeyCode touch;

	protected Animation animation;
	protected bool open = false;

	// Use this for initialization
	void Start () 
	{
		SMParametre.Parametre param = SMParametre.Parametre.Load ();
		touch = param.Key ["Interact"];
		this.animation = this.gameObject.GetComponent<Animation>();
	} 

	public void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (Input.GetKeyDown(this.touch) && !this.open)
			{
				Debug.Log ("open");
				open_Shelf ();
			}
		}
	}

	public void open_Shelf()
	{
		if (!this.open)
		{
			this.animation.Play("Move_Shelf");
			this.open = true;
		}
	}
}