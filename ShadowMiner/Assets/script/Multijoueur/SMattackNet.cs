using SMParametre;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMattackNet : MonoBehaviour {

    [SerializeField]
    private Animator anim;
    protected int inputattack;

    private int reloadtime = 0;// Use this for initialization
    void Start () {
        Parametre param = Parametre.Load();
        this.inputattack = param.Mouse["Attack"];
    }

    public void Update()
    {
        //anim.SetBool("attack", false);
        if (reloadtime > 0)
        {
            reloadtime--;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetMouseButtonDown(this.inputattack) && reloadtime == 0)
        {
            Debug.Log("SM attack");
            gameObject.GetComponentInParent<EntityNetwork>().attack(other.gameObject);
            //anim.SetBool("attack", true);
            reloadtime = 20;
        }
    }
}
