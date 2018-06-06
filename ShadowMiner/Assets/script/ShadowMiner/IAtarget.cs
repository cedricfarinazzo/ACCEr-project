using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class IAtarget : MonoBehaviour {

	[SerializeField]
	private NavMeshAgent _agent;
    [SerializeField]
    private Animator anim;

	[SerializeField] protected Vector3 startposition;
	[SerializeField] protected float roamRadius;

	private bool targetting = false;
	[SerializeField]
	private int _reloadTargetFree = 175;

    public bool attack = false;

    Vector3 lastpos = Vector3.zero;

    protected Vector3 Target;
	
	// Use this for initialization
	void Start ()
    {
        lastpos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!targetting && !attack)
		{
			if (_reloadTargetFree == 0)
			{
				FreeRoam();
				_reloadTargetFree = 100;
			}
			else
			{
				_reloadTargetFree--;
			}
		}

        if (attack)
        {
            _agent.SetDestination(gameObject.transform.position);
        }

        Vector3 delta = gameObject.transform.position - lastpos;
        anim.SetBool("jump", delta.y > 0);
        float lenght = new Vector2(delta.x, delta.z).magnitude;
        anim.SetBool("walk", 0 < lenght && lenght <= 0.3f);
        anim.SetBool("run", 0.3f < lenght);
        lastpos = gameObject.transform.position;
    }

    private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && !targetting && !attack)
		{
			Debug.Log("target: on");
			Target = other.transform.position;
            targetting = true;
            SetDestination(Target);
            _reloadTargetFree = 0;
		}

        if (attack)
        {
            _agent.SetDestination(gameObject.transform.position);
        }
    }

	
	private void OnTriggerExit(Collider other)
	{
        Debug.Log("target: off");
        targetting = false;
        _agent.SetDestination(gameObject.transform.position);
        
	}
	
	void FreeRoam()
	{
		
		Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
		randomDirection += startposition;
		NavMeshHit hit;
		NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
		Vector3 finalPosition = hit.position;     
		SetDestination(finalPosition);
	}

	void SetDestination(Vector3 target)
	{
		try
		{
			_agent.SetDestination(target);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}

    /*
    void OnAnimatorMove()
    {
        // Update position to agent position
        transform.position = _agent.nextPosition;
    }*/
}
