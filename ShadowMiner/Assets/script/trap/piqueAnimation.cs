using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piqueAnimation : MonoBehaviour {

    protected Animation anim;
    protected bool is_up = false;

	void Start () {
        this.anim = this.gameObject.GetComponentInChildren<Animation>();
	}
	
	void Update () {
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            if (!this.is_up)
            {
                this.Up();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (this.is_up)
            {
                this.Down();
            }
        }
    }

    public void Up()
    {
        if (!this.is_up)
        {
            this.anim.Play("uppique");
            this.is_up = true;
        }
    }

    public void Down()
    {
        if (this.is_up)
        {
            this.anim.Play("downpique");
            this.is_up = false;
        }
    }
}
