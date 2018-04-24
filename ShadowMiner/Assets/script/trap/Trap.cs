﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Entity {

    [SerializeField]
    private int reload_time;
    private int actual_time = 0;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void Death()
    {
        return;
    }

    public void Attack(GameObject other)
    {
        if (actual_time <= 0)
        {
            this.actual_time = this.reload_time;
            Entity entity = other.GetComponent<Entity>();
            entity.getDamage(this.damage);
        }
        else
        {
            this.actual_time--;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        this.actual_time = 0;
    }
}