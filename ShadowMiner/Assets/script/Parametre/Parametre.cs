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

	    public readonly string Version = "0.3";
        public Dictionary<string, KeyCode> Key = new Dictionary<string, KeyCode>()
        {
            { "MoveUp", KeyCode.Z},
            { "MoveDown", KeyCode.S },
            { "MoveLeft", KeyCode.Q },
            { "MoveRight", KeyCode.D },
            { "Run", KeyCode.LeftShift },
            { "Jump", KeyCode.Space},
            { "Interact", KeyCode.E }, 
	        { "Escape", KeyCode.Escape}
        };

        public Dictionary<string, int> Mouse = new Dictionary<string, int>()
        {
            { "Attack", 0 }
        };

		public float VolumeSonore = 1;
		public float Sensi = 1;
		public int Quality = 5; // <= 5
		public int Frequency = 60;
		public int Resolution = 8;
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
	        try
	        {
				QualitySettings.SetQualityLevel(this.Quality, true);
				Screen.SetResolution(ResolutionList[Resolution][0],
					ResolutionList[Resolution][1], FullScreen, Frequency);
	        }
	        catch (Exception e)
	        {
				Debug.Log("Settings error");
	        }

        }
    }

}

