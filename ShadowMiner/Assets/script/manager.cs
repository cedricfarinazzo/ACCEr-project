using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour {

    [SerializeField]
    protected GameObject g;
    [SerializeField]
    protected KeyCode key;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(key))
        {
            Instantiate(g, this.gameObject.transform.position + Vector3.up * 5, Quaternion.identity);
        }
	}
}
