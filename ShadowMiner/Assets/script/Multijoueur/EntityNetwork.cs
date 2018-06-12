using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityNetwork : Entity {

    protected virtual void Death()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    public void getDamage(int damage)
    {
        this.life -= damage;
        if (this.life <= 0)
        {
            this.Death();
        }
    }

    public void attack(GameObject target)
    {
        Entity e = target.GetComponent<Entity>();
        e.getDamage(this.damage);
        photonView.RPC("SyncDamage", PhotonTargets.Others, target.GetComponent<PhotonView>().viewID, damage);
    }

    [PunRPC]
    public void SyncDamage(int viewId, int damage)
    {
        GameObject go = PhotonView.Find(viewId).gameObject;
        if (go.GetComponent<PhotonView>().isMine)
        {
            go.GetComponent<EntityNetwork>().getDamage(damage);
        }
    }
}
