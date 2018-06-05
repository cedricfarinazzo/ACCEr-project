using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayName : MonoBehaviour {

    public GameObject nameLabel;

    void Start () {

	}

    private bool IsInView()
    {
        return gameObject.GetComponent<Renderer>().isVisible;
    }

     void Update () {
        if (IsInView())
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        }
        else
        {
            nameLabel.SetActive(true);
        }
    }
}
