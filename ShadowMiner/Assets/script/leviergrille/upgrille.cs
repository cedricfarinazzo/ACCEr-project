using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgrille : MonoBehaviour {

    protected KeyCode touche;
    protected bool Up = false;
    protected int progress = 55;

    // Use this for initialization
    void Start()
    {
        SMParametre.Parametre param = SMParametre.Parametre.Load();
        touche = param.Key["Interact"];
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !Up)
        {
            if (Input.GetKeyDown(this.touche))
            {
                Transform[] listedetransform = other.gameObject.GetComponentsInChildren<Transform>();
                bool havekey = false;
                foreach (Transform transfo in listedetransform)
                {
                    if (transfo.gameObject.name == "grille")
                    {
                        GameObject.Destroy(transfo.gameObject);
                        havekey = true;
                    }
                }
                Up = havekey;
            }
        }
    }

    public void Update()
    {
        if (Up)
        {
            if (progress == 0)
            {
                gameObject.GetComponent<upgrille>().enabled = false;
            }
            else
            {
                gameObject.transform.Translate(0, 0.1f, 0);
                progress--;
            }

        }
    }
}
