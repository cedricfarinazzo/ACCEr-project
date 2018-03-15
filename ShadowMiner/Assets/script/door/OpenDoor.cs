using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

    [SerializeField]
    protected KeyCode touche = KeyCode.E;

    protected Animation animation;
    protected bool open = false;

    private int timeout = 0;

	// Use this for initialization
	void Start () {
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

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(this.touche) && this.timeout == 0)
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

    public void opendoor()
    {
        if (!this.open)
        {
            this.animation.Play("opendoor");
            this.open = true;
        }
    }

    public void closedoor()
    {
        if (this.open)
        {
            this.animation.Play("closedoor");
            this.open = false;
        }
    }
}