using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateur : MonoBehaviour {

    public faux_sol fosaul;

    void OnTriggerEnter(Collider col)
    {
        GameObject jeuobjet = col.gameObject;
        if (jeuobjet.tag == "Monster")
        {
            fosaul.activation = true;
        }
    }
}
