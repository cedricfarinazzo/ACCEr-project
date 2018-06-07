using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAttack : MonoBehaviour {

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private IAtarget _agent;

    private int reloadtime = 0;

    public void Update()
    {
        if (reloadtime > 0)
        {
            reloadtime--;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && reloadtime == 0)
        {
            Debug.Log("SM attack");
            gameObject.GetComponentInParent<Entity>().attack(other.gameObject);
            anim.SetBool("attack", true);
            _agent.attack = true;
            reloadtime = 150;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        anim.SetBool("attack", false);
        _agent.attack = false;
    }
}
