using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonamiCode : MonoBehaviour {

    [SerializeField]
    protected string tokonami = "";

    private List<KeyCode> keylist = new List<KeyCode>()
    {
        KeyCode.UpArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.B,
        KeyCode.A
    };

    private int n = 0;
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(keylist[n]))
            {
                n++;
                if (n == keylist.Count)
                {
                    Konami();
                }
            
            }
            else
            {
                n = 0;
            }
        }

	}

    public void Konami()
    {
        Application.LoadLevel(tokonami);
    }
}
