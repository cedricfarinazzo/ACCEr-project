using System.Collections;
using System.Collections.Generic;
using SMParametre;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //data
    [SerializeField]
    protected int speedwalk;
    [SerializeField]
    protected int speedrun;
    [SerializeField]
    protected Vector3 forcejump;

    //inputs
    //[SerializeField]
    protected KeyCode inputright;
    //[SerializeField]
    protected KeyCode inputleft;
    //[SerializeField]
    protected KeyCode inputfront;
    //[SerializeField]
    protected KeyCode inputback;
    //[SerializeField]
    protected KeyCode inputjump;
    //[SerializeField]
    protected KeyCode inputrun;

    [SerializeField] protected Animator animator;
    
    //animation component
    //protected Animation anim;

    //feet collider
    protected CapsuleCollider playercollider;

    //rigidbody
    protected Rigidbody ri;

    // Use this for initialization
    void Start () {
        //this.anim = this.gameObject.GetComponentInChildren<Animation>();
        Parametre param = Parametre.Load();
        this.inputfront = param.Key["MoveUp"];
        this.inputback = param.Key["MoveDown"];
        this.inputleft = param.Key["MoveLeft"];
        this.inputright = param.Key["MoveRight"];
        this.inputjump = param.Key["Jump"];
        this.inputrun = param.Key["Run"];
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
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
            animator.SetBool("jump", false);
            //this.anim.Play("idle");
        }

        if (Input.GetKey(this.inputleft))
        {
            this.gameObject.transform.Translate(-(this.speedwalk / 2) * Time.deltaTime, 0, 0);
            //animator.SetBool("walk", true);
            //this.anim.Play("walk");
        }

        if (Input.GetKey(this.inputright))
        {
            this.gameObject.transform.Translate((this.speedwalk / 2) * Time.deltaTime, 0, 0);
            //animator.SetBool("walk", true);
            //this.anim.Play("walk");
        }

        if (Input.GetKey(this.inputfront) && !Input.GetKey(this.inputrun))
        {
            this.gameObject.transform.Translate(0, 0, this.speedwalk * Time.deltaTime);
            if (Input.GetKeyDown(this.inputjump) && IsGrounded())
            {
                animator.SetBool("jump", true);
            }
            else
            {
                animator.SetBool("walk", true);
                animator.SetBool("run", false);
                animator.SetBool("jump", false);
            }
            //this.anim.Play("walk");
        }

        if (Input.GetKey(this.inputfront) && Input.GetKey(this.inputrun))
        {
            this.gameObject.transform.Translate(0, 0, this.speedrun * Time.deltaTime);
            //this.anim.Play("run");
            if (Input.GetKeyDown(this.inputjump) && IsGrounded())
            {
                animator.SetBool("jump", true);
            }
            else
            {
                animator.SetBool("walk", false);
                animator.SetBool("run", true);
                animator.SetBool("jump", false);
            }
            
        }

        if (Input.GetKey(this.inputback))
        {
            this.gameObject.transform.Translate(0, 0, -(this.speedwalk / 2) * Time.deltaTime);
            //this.anim.Play("walk");
            if (Input.GetKeyDown(this.inputjump) && IsGrounded())
            {
                animator.SetBool("jump", true);
            }
            else
            {
                animator.SetBool("walk", true);
                animator.SetBool("run", false);
                animator.SetBool("jump", false);
            }

        }
    }

    protected void jump()
    {
        if (Input.GetKeyDown(this.inputjump) && IsGrounded())
        {
            Vector3 v = this.gameObject.GetComponent<Rigidbody>().velocity;
            v.y = this.forcejump.y;

            this.gameObject.GetComponent<Rigidbody>().velocity = this.forcejump;
            animator.SetBool("jump", true);
            //this.anim.Play("jump_pose");
        }
    }
}
