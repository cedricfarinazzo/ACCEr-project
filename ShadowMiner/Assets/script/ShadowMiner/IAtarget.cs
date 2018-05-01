using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAtarget : MonoBehaviour {

	[SerializeField]
	private NavMeshAgent agent;

	[SerializeField] protected Vector3 startposition;
	[SerializeField] protected float roamRadius;

	private bool targetting = false;
	[SerializeField]
	private int reload_target_free = 175;

	protected Vector3 target;
	
	// Use this for initialization
	void Start ()
	{
		target = Vector3.zero;
		agent.SetDestination(target);
	}
	
	// Update is called once per frame
	void Update () {
		if (targetting)
		{
			agent.SetDestination(target);
		}
		else
		{
			if (reload_target_free == 0)
			{
				FreeRoam();
				reload_target_free = 100;
			}
			else
			{
				reload_target_free--;
			}
		}
		
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && !targetting)
		{
			Debug.Log("target: on");
			target = other.transform.position;
		}
	}

	/*
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && targetting)
		{
			Debug.Log("target: off");
			target = Vector3.zero;
		}
	}*/
	
	void FreeRoam()
	{
		
		Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
		randomDirection += startposition;
		NavMeshHit hit;
		NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
		Vector3 finalPosition = hit.position;     
		agent.destination = finalPosition;
	}
}
