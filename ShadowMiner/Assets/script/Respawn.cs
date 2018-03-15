using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    [SerializeField]
    protected GameObject target;

	void OnCollisionEnter(Collision other)
    {
        other.transform.Translate(0, (this.target.transform.position - this.transform.position).y + 1f, 0);
    }
}
