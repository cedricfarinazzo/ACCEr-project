using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keydoorscript : OpenDoor {

	protected bool havekey = true;
	protected GameObject _key;

	// Use this for initialization
	void Start () {
		SMParametre.Parametre param = SMParametre.Parametre.Load ();
		touche = param.Key ["Interact"];
		this._g = this.gameObject;
		this.animation = this.gameObject.GetComponentInChildren<Animation>();
	}

	// Update is called once per frame
	void Update()
	{
		if (this.timeout > 0)
		{
			this.timeout--;
		}
	}

	public new void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (Input.GetKeyDown(this.touche) && this.timeout == 0 && havekey)
			{
				if (!this.open)
				{
					this.opendoor();
				}
				else
				{
					this.closedoor();
				}
				this.timeout = 50;
			}
		}
	}

}
