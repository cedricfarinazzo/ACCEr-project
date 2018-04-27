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
		public KeyCode Avancer = KeyCode.Z;
		public KeyCode Reculer = KeyCode.S;
		public KeyCode Droite = KeyCode.D;
		public KeyCode Gauche = KeyCode.Q;
		public KeyCode Sauter = KeyCode.Space;
		public KeyCode Accroupir = KeyCode.LeftAlt;
		public KeyCode Attaquer = KeyCode.Mouse0;
		public KeyCode Interagir = KeyCode.E;
		public int Volumesonore = 1;
		public int Sensibilité = 1;
		public string Qualité = "low";
		public int Fréquence = 30;
		public int[] Résolution = {720,1080};
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

