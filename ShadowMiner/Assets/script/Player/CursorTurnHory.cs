using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTurnHory : MonoBehaviour {

    [SerializeField]
    protected int turnspeed;

    private Transform _t;

	void Start () {
        this._t = this.transform;
	}
	
	void FixedUpdate () {
        this._t.Rotate(Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * this.turnspeed, Space.Self);
    }
}
