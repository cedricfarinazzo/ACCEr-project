using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

    [SerializeField]
    protected KeyCode touche = KeyCode.E;

    protected Animation animation;
    protected bool open = false;

	// Use this for initialization
	void Start () {
        this.animation = this.gameObject.GetComponent<Animation>();
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(this.touche))
            {
                if (!this.open)
                {
                    this.opendoor();
                }
                else
                {
                    this.closedoor();
                }
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