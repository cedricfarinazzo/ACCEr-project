using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentController : MonoBehaviour
{

	[SerializeField] protected Transform target;
	
	void Start ()
	{
		NavMeshAgent agent = this.gameObject.GetComponent<NavMeshAgent>();
		agent.SetDestination(target.position);
	}
}
