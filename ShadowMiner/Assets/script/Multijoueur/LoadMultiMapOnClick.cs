using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMultiMapOnClick : MonoBehaviour
{
    public void OnClick()
    {
        string name = gameObject.transform.parent.name;
        SaveData.SaveData.SaveString("Multi.mode", name);
        SaveData.SaveData.SaveString("Loader.Next", "lobby");
        SceneManager.LoadScene("loading");
    }
}
