using SMParametre;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuController : MonoBehaviour {

    [SerializeField] protected Animator animator;
    protected AudioSource _audioSource;

    [SerializeField] protected AudioClip[] sound;
    protected int Walksoundfrequency = 25;
    protected int Runsoundfrequency = 15;

    RaycastHit hitInfo = new RaycastHit();
    UnityEngine.AI.NavMeshAgent agent;

    Vector3 lastpos = Vector3.zero;

    // Use this for initialization
    void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        this._audioSource = this.gameObject.GetComponent<AudioSource>();
        lastpos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update () {
        //reset
        animator.SetBool("walk", false);
        animator.SetBool("run", false);
        animator.SetBool("jump", false);

        //update destination
        /*if (Input.GetMouseButtonDown(0))
        {*/
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            agent.destination = hitInfo.point;
        //}

        //update anim
        Vector3 delta = gameObject.transform.position - lastpos;
        animator.SetBool("jump", delta.y > 0);
        float lenght = new Vector2(delta.x, delta.z).magnitude;
        animator.SetBool("walk", 0 < lenght && lenght <= 0.3f);
        animator.SetBool("run", 0.3f < lenght);
        lastpos = gameObject.transform.position;
    }

    protected void PlayWalkClip()
    {
        if (Walksoundfrequency == 0)
        {
            Walksoundfrequency = 25;
            int n = Random.Range(1, 4);
            _audioSource.clip = sound[n];
            _audioSource.PlayOneShot(_audioSource.clip);
            sound[n] = sound[0];
            sound[0] = _audioSource.clip;
        }
        else
        {
            Walksoundfrequency--;
        }
    }

    protected void PlayRunClip()
    {
        if (Runsoundfrequency == 0)
        {
            Runsoundfrequency = 15;
            int n = Random.Range(1, 4);
            _audioSource.clip = sound[n];
            _audioSource.PlayOneShot(_audioSource.clip);
            sound[n] = sound[0];
            sound[0] = _audioSource.clip;
        }
        else
        {
            Runsoundfrequency--;
        }
    }

    protected void PlayJumpClip()
    {
        _audioSource.clip = sound[4];
        _audioSource.PlayOneShot(_audioSource.clip);
    }
}
