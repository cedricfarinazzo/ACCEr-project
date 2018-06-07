using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinematiquemine : MonoBehaviour {

    [SerializeField] protected GameObject Target;
    [SerializeField] protected Animation anim;
    [SerializeField] protected float speed;

    [SerializeField] private bool open = false;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Target.transform);
        transform.Translate(0, 0, Time.deltaTime * speed);
        speed += 0.1f;
        Vector3 delta = gameObject.transform.position - Target.transform.position;
        if (delta.magnitude < 100 && !open)
        {
            open = true;
            anim.Play("opendoor");
        }
    }
}
