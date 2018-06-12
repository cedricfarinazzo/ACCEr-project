using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorNetwork : Photon.MonoBehaviour {

    protected KeyCode touche;

    protected Animator animation;
    protected GameObject _g;
    public bool open = false;

    protected int timeout = 0;

    // Use this for initialization
    void Start()
    {
        SMParametre.Parametre param = SMParametre.Parametre.Load();
        touche = param.Key["Interact"];
        this._g = this.gameObject;
        this.animation = this.gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.animation.SetBool("open", open);
        this.animation.SetBool("close", !open);
        _g.GetComponent<BoxCollider>().enabled = !open;
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
            //this.animation.SetBool("open", true);
            //_g.GetComponent<BoxCollider>().enabled = false;
            this.open = true;
            photonView.RPC("ChangeState", PhotonTargets.Others);
        }
    }

    [PunRPC]
    public void ChangeState()
    {
        open = !open;
    }

    public void closedoor()
    {
        if (this.open)
        {
            //this.animation.SetBool("close", true);
            //_g.GetComponent<BoxCollider>().enabled = true;
            this.open = false;
            photonView.RPC("ChangeState", PhotonTargets.Others);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            open = (bool)stream.ReceiveNext();
        }
        else
        {
            stream.SendNext(open);
        }
    }
}
