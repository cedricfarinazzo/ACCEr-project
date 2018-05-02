using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTurnHory : MonoBehaviour {

    [SerializeField]
    protected int turnspeed;

    private Transform _t;
	
	private float sensi;

	void Start () {
		sensi = SMParametre.Parametre.Load().Sensi;
        this._t = this.transform;
	}
	
	void FixedUpdate () {
        this._t.Rotate(Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * (this.turnspeed *  sensi), Space.Self);
    }
}
