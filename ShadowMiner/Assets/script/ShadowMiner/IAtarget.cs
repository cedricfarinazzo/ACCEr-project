using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAtarget : MonoBehaviour {

	[SerializeField]
	private NavMeshAgent agent;

	protected Vector3 target;
	
	// Use this for initialization
	void Start ()
	{
		target = this.gameObject.transform.position;
		agent.SetDestination(target);
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination(target);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Debug.Log("target: on");
			target = other.transform.position;
		}
	}

	/*
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			target = this.gameObject.transform.position;
		}
	}*/
}
