using System.Collections;
using System.Collections.Generic;
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
    protected int fall_damage = 42;
    protected int reload_fall = 50;

    [SerializeField] protected Image LifeBar;

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
        Debug.Log("velocity y : " + this.g.GetComponent<Rigidbody>().velocity.y + "   life : " + this.life);
        if (LifeBar != null)
        {
            UpdateLifeBar();    
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

    public void FallDamage()
    {
        if (this.g.GetComponent<Rigidbody>().velocity.y < -40 && this.reload_fall == 50)
        {
            this.g.GetComponent<Entity>().getDamage(this.fall_damage);
            this.reload_fall = 0;
        }
        if (this.reload_fall != 50)
        {
            this.reload_fall++;
        }
    }
}