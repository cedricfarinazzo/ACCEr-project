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
        public KeyCode Courir = KeyCode.LeftShift;
		public KeyCode Attaquer = KeyCode.Mouse0;
		public KeyCode Interagir = KeyCode.E;
		public float VolumeSonore = 1;
		public float Sensi = 1;
		public int Quality = 5; // <= 5
		public int Frequency = 60;
		public int[] Resolution = {1650,1050};
        public bool Windowed = true;

	    public Parametre()
	    {
	    }
		
	    public static Parametre Load()
	    {
            return SaveData.SaveData.GetObject<Parametre>("Parametre");
	    }

	    public void Save()
	    {
            SaveData.SaveData.SaveObject<Parametre>("Parametre", this);
	    }
    }

}

