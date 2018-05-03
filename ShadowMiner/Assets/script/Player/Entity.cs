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
    protected float fall_damage = 10f;

    [SerializeField] protected Image LifeBar;
    [SerializeField] protected GameObject canvasLife;

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

    protected void attack(GameObject target)
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
    }

    public void UpdateLifeBar()
    {
        float progress = (float) life / (float) max_life;
        LifeBar.fillAmount = progress;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
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
    
    bool IsGrounded()
    {
        Vector3 dwn = this.gameObject.transform.TransformDirection(Vector3.down);
        return Physics.Raycast(this.transform.position, dwn, this.gameObject.transform.lossyScale.y);
    }

    public void FallDamage()
    {
        if (LastPost > this.gameObject.transform.position.y)
        {
            fallDist += LastPost - this.gameObject.transform.position.y;
        }

        if (fallDist < 5 && IsGrounded())
        {
            fallDist = 0;
            LastPost = 0;
            Debug.Log("Fail without damage");
        }

        if (fallDist >= 5 && IsGrounded())
        {
            fallDist = 0;
            LastPost = 0;
            float damage = fall_damage; //(fall_damage *  (fallDist / 2f));
            int intdamage = 0;
            if (damage % 1 >= 0.5)
            {
                intdamage = (int) damage + 1;
            }
            else
            {
                intdamage = (int) damage;
            }

            life -= intdamage;
            Debug.Log("Fail with damage");
        }
    }
}