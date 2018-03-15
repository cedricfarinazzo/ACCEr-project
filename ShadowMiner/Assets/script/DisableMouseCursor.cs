using UnityEngine;
using System.Collections;

public class DisableMouseCursor : MonoBehaviour {

	void Start()
	{
		Cursor.visible = false;
        Cursor.lockState = UnityEngine.CursorLockMode.Locked;
	}
}
