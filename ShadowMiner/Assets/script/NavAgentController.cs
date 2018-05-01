using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentController : MonoBehaviour
{

	[SerializeField] protected Transform target;
	private NavMeshAgent agent;
	
	void Start ()
	{
		agent = this.gameObject.GetComponent<NavMeshAgent>();
		agent.SetDestination(target.position);
	}

	private void Update()
	{
		agent.SetDestination(target.position);
	}
}
