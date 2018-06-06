using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownLevier : MonoBehaviour {

    protected KeyCode touche;
    [SerializeField]
    protected GameObject bar;
    protected bool Down = false;
    protected int progress = 50;

    // Use this for initialization
    void Start () {
        SMParametre.Parametre param = SMParametre.Parametre.Load();
        touche = param.Key["Interact"];
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !Down)
        {
            if (Input.GetKeyDown(this.touche))
            {
                GameObject grille = GameObject.Instantiate(new GameObject());
                grille.name = "grille";
                grille.transform.SetParent(other.gameObject.transform);
                Down = true;
            }
        }
    }

    public void Update()
    {
        if (Down)
        {
            if (progress == 0)
            {
                gameObject.GetComponent<DownLevier>().enabled = false;
            }
            else
            {
                bar.transform.Rotate(1, 0, 0);
                progress--;
            }

        }
    }
}
