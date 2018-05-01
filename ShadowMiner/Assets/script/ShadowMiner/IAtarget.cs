﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class IAtarget : MonoBehaviour {

	[SerializeField]
	private NavMeshAgent _agent;

	[SerializeField] protected Vector3 startposition;
	[SerializeField] protected float roamRadius;

	private bool targetting = false;
	[SerializeField]
	private int _reloadTargetFree = 175;

	protected Vector3 Target;
	
	// Use this for initialization
	void Start ()
	{
		Target = Vector3.zero;
		SetDestination(Target);
	}
	
	// Update is called once per frame
	void Update () {
		if (targetting)
		{
			SetDestination(Target);
		}
		else
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
		
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && !targetting)
		{
			Debug.Log("target: on");
			Target = other.transform.position;
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
}