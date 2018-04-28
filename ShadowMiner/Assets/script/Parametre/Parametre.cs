using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveData;

namespace SMParametre
{
    enum Frequency
    {
        Low = 30,
        Hight = 60
    }

    enum GraphicsQuality
    {
        VeryLow = 0,
        Low = 1,
        Medium = 2,
        Hight = 3,
        VeryHight = 4,
        Ultra = 5
    }

    [Serializable]
    public class Parametre {

        public static List<int[]> ResolutionList = new List<int[]>()
        {
            new int[2] {320, 200},
            new int[2] {320, 240},
            new int[2] {400, 300},
            new int[2] {512, 384},
            new int[2] {640, 400},
            new int[2] {640, 480},
            new int[2] {800, 600},
            new int[2] {1024, 768},
            new int[2] {1152, 864},
            new int[2] {1280, 600},
            new int[2] {1280, 720},
            new int[2] {1280, 768},
            new int[2] {1280, 800},
            new int[2] {1280, 960},
            new int[2] {1280, 1024},
            new int[2] {1360, 768},
            new int[2] {1366, 768},
            new int[2] {1400, 1050},
            new int[2] {1440, 900},
            new int[2] {1600, 900},
            new int[2] {1680, 1050},
        };

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
		public int Resolution = 20;
        public bool FullScreen = true;

	    public Parametre()
	    {
	    }
		
	    public static Parametre Load()
	    {
            Parametre p = SaveData.SaveData.GetObject<Parametre>("Parametre");
            if (p == null)
            {
                return new Parametre();
            }
            return p;
	    }

	    public void Save()
	    {
            SaveData.SaveData.SaveObject("Parametre", this);
	    }

        public void Apply()
        {
            QualitySettings.SetQualityLevel(this.Quality, true);
            Screen.SetResolution(ResolutionList[Resolution][0],
                ResolutionList[Resolution][1], FullScreen, Frequency);
        }
    }

}

