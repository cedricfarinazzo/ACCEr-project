﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //data
    [SerializeField]
    protected int speedwalk;
    [SerializeField]
    protected int speedrun;
    [SerializeField]
    protected int speedturn;
    [SerializeField]
    protected Vector3 forcejump;

    //inputs
    [SerializeField]
    protected KeyCode inputright;
    [SerializeField]
    protected KeyCode inputleft;
    [SerializeField]
    protected KeyCode inputfront;
    [SerializeField]
    protected KeyCode inputback;
    [SerializeField]
    protected KeyCode inputjump;
    [SerializeField]
    protected KeyCode inputrun;

    //animation component
    protected Animation anim;

    //feet collider
    protected CapsuleCollider playercollider;

    //rigidbody
    protected Rigidbody ri;

    // Use this for initialization
    void Start () {
        this.anim = this.gameObject.GetComponentInChildren<Animation>();
        this.playercollider = this.gameObject.GetComponent<CapsuleCollider>();
        this.ri = this.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        move();
        jump();

        Debug.DrawRay(transform.position, this.gameObject.transform.TransformDirection(Vector3.down) * (this.gameObject.transform.lossyScale.y), Color.red);
    }

    void FixedUpdate()
    {
        this.ri.AddForce(Vector3.down * Physics.gravity.x * ri.mass);
    }

    bool IsGrounded()
    {
        Vector3 dwn = this.gameObject.transform.TransformDirection(Vector3.down);
        return Physics.Raycast(this.transform.position, dwn, this.gameObject.transform.lossyScale.y);
    }

    protected void move()
    {
        if (!Input.GetKey(this.inputback) && !Input.GetKey(this.inputfront) && ! Input.GetKey(this.inputjump))
        {
            this.anim.Play("idle");
        }

        if (Input.GetKey(this.inputleft))
        {
            this.gameObject.transform.Translate(-(this.speedwalk / 2) * Time.deltaTime, 0, 0);
            //this.anim.Play("walk");
        }

        if (Input.GetKey(this.inputright))
        {
            this.gameObject.transform.Translate((this.speedwalk / 2) * Time.deltaTime, 0, 0);
            //this.anim.Play("walk");
        }

        if (Input.GetKey(this.inputfront) && !Input.GetKey(this.inputrun))
        {
            this.gameObject.transform.Translate(0, 0, this.speedwalk * Time.deltaTime);
            this.anim.Play("walk");
        }

        if (Input.GetKey(this.inputfront) && Input.GetKey(this.inputrun))
        {
            this.gameObject.transform.Translate(0, 0, this.speedrun * Time.deltaTime);
            this.anim.Play("run");
        }

        if (Input.GetKey(this.inputback))
        {
            this.gameObject.transform.Translate(0, 0, -(this.speedwalk / 2) * Time.deltaTime);
            this.anim.Play("walk");
        }
    }

    protected void jump()
    {
        if (Input.GetKeyDown(this.inputjump) && IsGrounded())
        {
            Vector3 v = this.gameObject.GetComponent<Rigidbody>().velocity;
            v.y = this.forcejump.y;

            this.gameObject.GetComponent<Rigidbody>().velocity = this.forcejump;
            //this.anim.Play("jump_pose");
        }
    }
}