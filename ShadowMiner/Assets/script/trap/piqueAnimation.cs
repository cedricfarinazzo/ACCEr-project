using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piqueAnimation : Trap {

    protected Animation anim;
    protected bool is_up = false;

	void Start () {
        this.anim = this.gameObject.GetComponentInChildren<Animation>();
	}
	
	void Update () {
	    if (this.actual_time > 0)
	    {
	        this.actual_time--;
	    }
	}


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            if (!this.is_up)
            {
                Attack(other.gameObject);
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
