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

