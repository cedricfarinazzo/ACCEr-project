using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

    protected SMParametre.Parametre param;

    [SerializeField]
    protected SceneAsset Next;

    private AsyncOperation progress;

    // Use this for initialization
    void Start () {
        param = SMParametre.Parametre.Load();
        param.Apply();
        progress = SceneManager.LoadSceneAsync(Next.name);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(progress.progress);
	}
}
