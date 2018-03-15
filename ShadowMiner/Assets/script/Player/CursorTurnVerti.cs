﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTurnVerti : MonoBehaviour {

    [SerializeField]
    protected int turnspeed;

    private Transform _t;

    void Start()
    {
        this._t = this.transform;
    }

    
    void Update () {
        var euler = this._t.localRotation.eulerAngles;

        float newAngle = euler.x - Input.GetAxis("Mouse Y") * Time.deltaTime * this.turnspeed;

        this._t.localRotation =
            Quaternion.Euler(
                Mathf.Clamp(newAngle <= 180f ? newAngle : newAngle - 360f, -90f, 90f)
                             , euler.y
                             , euler.z);
    }
}