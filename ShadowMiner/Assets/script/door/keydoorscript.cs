using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keydoorscript : OpenDoor {
	
	public void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Transform[] listedetransform = other.gameObject.GetComponentsInChildren<Transform> ();
			bool havekey = false;
			foreach (Transform transfo in listedetransform) {
					if(transfo.gameObject.name == "key")
					{
						havekey = true;
					}
			}
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
