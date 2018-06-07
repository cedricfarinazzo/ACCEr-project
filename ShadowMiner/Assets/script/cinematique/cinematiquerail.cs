using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinematiquerail : MonoBehaviour {

    [SerializeField] protected GameObject Target;
    [SerializeField] protected GameObject chariot;
    [SerializeField] protected GameObject tomove;

    [SerializeField] protected float speed;

    // Use this for initialization
    void Start () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 delta = gameObject.transform.position - Target.transform.position;
        transform.LookAt(chariot.transform);
        tomove.transform.Translate(0, 0, Time.deltaTime * speed);
        if (speed < 4f)
        {
            speed += 0.09f;
        }

        if (delta.magnitude < 15)
        {
            tomove.transform.position = new Vector3(9, 2, -4);
        }
        
    }
}
