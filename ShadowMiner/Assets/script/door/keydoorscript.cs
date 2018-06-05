using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keydoorscript : OpenDoor {

	protected bool havekey = false;
	protected GameObject _key;

	void Update()
	{
		if (this.timeout > 0)
		{
			this.timeout--;
		}
		if (Input.GetKeyDown (this.touche) && _key.gameObject.tag == "Player") {
			havekey = true;
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
