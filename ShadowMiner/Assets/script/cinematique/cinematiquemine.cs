using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cinematiquemine : MonoBehaviour {

    [SerializeField] protected string scene;

    [SerializeField] protected GameObject Target;
    [SerializeField] protected Animation anim;
    [SerializeField] protected float speed;

    [SerializeField] private bool open = false;

    [SerializeField] private bool menu = false;
    [SerializeField] private int timebeforemenu = 100;

    // Use this for initialization
	void Start () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            ChangeScene();
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        Vector3 delta = gameObject.transform.position - Target.transform.position;
        transform.LookAt(Target.transform);
        if (delta.magnitude > 40)
        {
            transform.Translate(0, 0, Time.deltaTime * speed);
            speed += 0.2f;
        }
        else
        {
            menu = true;
        }

        if (delta.magnitude < 100 && !open)
        {
            open = true;
            anim.Play("opendoor");
        }

        if (menu)
        {
            timebeforemenu--;
        }

        if (timebeforemenu == 0 && menu)
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        SaveData.SaveData.SaveString("Loader.Next", scene);
        SceneManager.LoadScene("loading");
    }
}
