using SMParametre;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParametreKey : MonoBehaviour {

    protected SMParametre.Parametre param;
    [SerializeField]
    protected Slider Sensi;
    [SerializeField]
    protected Text Up, Down, Right, Left, Run, Jump, Interact, Attack;

    // Use this for initialization
    void Start()
    {
        param = SMParametre.Parametre.Load();
        param.Apply();
        Sensi.value = param.Sensi;
        Sensi.onValueChanged.AddListener(Change);
        Up.text = param.Avancer.ToString();
        Down.text = param.Reculer.ToString();
        Left.text = param.Gauche.ToString();
        Right.text = param.Droite.ToString();
        Run.text = param.Courir.ToString();
        Jump.text = param.Sauter.ToString();
        Interact.text = param.Interagir.ToString();
        Attack.text = param.Attaquer.ToString();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public void Change(float arg0)
    {
        param.Sensi = Sensi.value;
        param.Save();
    }
}
