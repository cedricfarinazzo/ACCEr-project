using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveData;

namespace SMParametre
{
    [Serializable]
    public class Parametre {

	    public string Version = "0.2";
		public enum Controls {
			avancer = KeyCode.Z,
			reculer = KeyCode.S,
			droite = KeyCode.D,
			gauche = KeyCode.Q,
			sauter = KeyCode.Space,
			accroupir = KeyCode.LeftAlt,
			attaquer = KeyCode.Mouse0,
			interagir = KeyCode.E}

		public int volumesonore;
		public int sensibilité;
		public string qualité;
		public int fréquence;
		public int[] résolution;
	    public Parametre()
	    {
	    }
		
	    public static void Load()
	    {
		    // sera appélé par beaucoup de script
		    // recupère depuis SaveData les données sauvegardées et retourne un objet Parametre
		    // key = "Parametre"
	    }

	    public void Save()
	    {
		    //enregistre grace à savedata
		    // key = "Parametre"
	    }
    }

}

