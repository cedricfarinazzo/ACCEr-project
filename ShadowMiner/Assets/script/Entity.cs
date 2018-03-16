using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : Photon.MonoBehaviour {

    [SerializeField]
    protected int life;
    [SerializeField]
    protected int max_life;
    protected int damage;

    protected GameObject g;

    void Start()
    {
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

    protected void Death()
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
}