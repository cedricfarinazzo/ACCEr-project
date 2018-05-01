using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using SMParametre;
using UnityEngine;

public class PlayerController : Photon.MonoBehaviour {

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

    private int reloadjump = 20;

    [SerializeField] protected Animator animator;
    protected AudioSource _audioSource;

    [SerializeField] protected AudioClip[] sound;
    protected int Walksoundfrequency = 25;
    protected int Runsoundfrequency = 15;
    
    //animation component
    //protected Animation anim;

    //feet collider
    protected CapsuleCollider playercollider;

    //rigidbody
    protected Rigidbody ri;

    // Use this for initialization
    void Start () {
        //this.anim = this.gameObject.GetComponentInChildren<Animation>();
        this._audioSource = this.gameObject.GetComponent<AudioSource>();
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
	    if (PhotonNetwork.connectedAndReady)
	    {
            if (photonView.isMine)
            {
                 move();
                 jump();
            }
	    }
	    else
	    {
	        move();
	        jump();
	    }


	    if (reloadjump < 20)
	    {
	        reloadjump++;
	    }
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
                jump();
            }
            else
            {
                PlayWalkClip();
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
                jump();
            }
            else
            {
                PlayRunClip();
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
                jump();
            }
            else
            {
                PlayWalkClip();
                animator.SetBool("walk", true);
                animator.SetBool("run", false);
                animator.SetBool("jump", false);

            }
        }
        
    }

    protected void jump()
    {
        if (Input.GetKey(this.inputjump))
         {
             animator.SetBool("jump", false);
         }
        if (reloadjump == 20)
        {
            reloadjump = 0;
            if (Input.GetKey(this.inputjump) && IsGrounded())
            {
                PlayJumpClip();
                Vector3 v = this.gameObject.GetComponent<Rigidbody>().velocity;
                v.y = this.forcejump.y;

                this.gameObject.GetComponent<Rigidbody>().velocity = this.forcejump;
                animator.SetBool("jump", true);
                //this.anim.Play("jump_pose");
            }
        }
    }

    protected void PlayWalkClip()
    {
        if (Walksoundfrequency == 0)
        {
            Walksoundfrequency = 25;
            int n = Random.Range(1, 4);
            _audioSource.clip = sound[n];
            _audioSource.PlayOneShot(_audioSource.clip);
            sound[n] = sound[0];
            sound[0] = _audioSource.clip;
        }
        else
        {
            Walksoundfrequency--;
        }
    }

    protected void PlayRunClip()
    {
        if (Runsoundfrequency == 0)
        {
            Runsoundfrequency = 15;
            int n = Random.Range(1, 4);
            _audioSource.clip = sound[n];
            _audioSource.PlayOneShot(_audioSource.clip);
            sound[n] = sound[0];
            sound[0] = _audioSource.clip;
        }
        else
        {
            Runsoundfrequency--;
        }
    }

    protected void PlayJumpClip()
    {
        _audioSource.clip = sound[4];
        _audioSource.PlayOneShot(_audioSource.clip);
    }
}
