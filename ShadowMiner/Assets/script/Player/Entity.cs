using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Entity : Photon.MonoBehaviour {

    [SerializeField]
    protected int life;
    [SerializeField]
    protected int max_life;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected int fall_damage = 10;

    [SerializeField] protected Image LifeBar;
    [SerializeField] protected GameObject canvasLife;
    [SerializeField] protected int maxchute;

    private float LastPost;
    private float fallDist;

    protected GameObject g;

    void Start()
    {
        fallDist = 0;
        LastPost = this.gameObject.transform.position.y;
        canvasLife.SetActive(true);
        this.g = this.gameObject;
    }

    public void attack(GameObject target)
    {
        Entity e = target.GetComponent<Entity>();
        e.getDamage(this.damage);
    }

    public void getDamage(int damage)
    {
        this.life -= damage;
        if (this.life <= 0)
        {
            this.Death();
        }
    }

    protected virtual void Death()
    {
        Destroy(this.gameObject);
    }

    public void Health(int h)
    {
        if (this.life + h <= this.max_life)
        {
            this.life += h;
        }
    }

    private void Update()
    {
        FallDamage();
        //Debug.Log("velocity y : " + this.g.GetComponent<Rigidbody>().velocity.y + "   life : " + this.life);
        if (LifeBar != null)
        {
            UpdateLifeBar();    
        }
        if (this.life <= 0)
        {
            this.Death();
        }
        UpdateLifeBar();
        LastPost = this.gameObject.transform.position.y;
    }

    public void UpdateLifeBar()
    {
        float progress = (float) life / (float) max_life;
        LifeBar.fillAmount = progress;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (PhotonNetwork.connectedAndReady)
        {
            if (stream.isWriting)
            {
                stream.SendNext(this.damage);
                stream.SendNext(this.life);
            }
            else
            {
                this.damage = (int)stream.ReceiveNext();
                this.life = (int)stream.ReceiveNext();
            }
        }
        
    }
    
    bool IsGrounded()
    {
        Vector3 dwn = this.gameObject.transform.TransformDirection(Vector3.down);
        return Physics.Raycast(this.transform.position, dwn, this.gameObject.transform.lossyScale.y);
    }

    public void FallDamage()
    {
        //Debug.Log(fallDist);
        if (LastPost > this.gameObject.transform.position.y)
        {
            fallDist += LastPost - this.gameObject.transform.position.y;
        }

        if (fallDist < maxchute && IsGrounded())
        {
            //fallDist = 0;
            //LastPost = 0;
            //Debug.Log("Fail without damage");
        }

        if (fallDist >= maxchute && IsGrounded())
        {
            //fallDist = 0;
            //LastPost = 0;
            life -= fall_damage + (int)(Mathf.Sqrt(fallDist*maxchute))*2;
            Debug.Log("Fail with damage : " + fall_damage + "  " + life);
        }

        if (IsGrounded())
        {
            LastPost = this.gameObject.transform.position.y;
            fallDist = 0;
        }
    }
}