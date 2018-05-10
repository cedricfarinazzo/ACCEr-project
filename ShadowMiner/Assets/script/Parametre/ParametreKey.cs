using SMParametre;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParametreKey : MonoBehaviour {

    protected SMParametre.Parametre param;
    [SerializeField]
    protected Slider Sensi;
    [SerializeField]
    protected GameObject Up, Down, Right, Left, Run, Jump, Interact, Attack;
    protected Button UpButton, DownButton, RightButton, LeftButton, RunButton, JumpButton, InteractButton, AttackButton;
    protected Text UpText, DownText, RightText, LeftText, RunText, JumpText, InteractText, AttackText;

    [SerializeField] protected Button ResetButton;

    protected string currentkey = null;

    // Use this for initialization
    void Start()
    {
        param = SMParametre.Parametre.Load();
        param.Apply();

        UpButton = Up.GetComponentInChildren<Button>();
        DownButton = Down.GetComponentInChildren<Button>();
        RightButton = Right.GetComponentInChildren<Button>();
        LeftButton = Left.GetComponentInChildren<Button>();
        RunButton = Run.GetComponentInChildren<Button>();
        JumpButton = Jump.GetComponentInChildren<Button>();
        InteractButton = Interact.GetComponentInChildren<Button>();
        AttackButton = Attack.GetComponentInChildren<Button>();

        UpText = UpButton.gameObject.GetComponentInChildren<Text>();
        DownText = DownButton.gameObject.GetComponentInChildren<Text>();
        LeftText = LeftButton.gameObject.GetComponentInChildren<Text>();
        RightText = RightButton.gameObject.GetComponentInChildren<Text>();
        RunText = RunButton.gameObject.GetComponentInChildren<Text>();
        JumpText = JumpButton.gameObject.GetComponentInChildren<Text>();
        AttackText = AttackButton.gameObject.GetComponentInChildren<Text>();
        InteractText = InteractButton.gameObject.GetComponentInChildren<Text>();

        Sensi.value = param.Sensi;
        Sensi.onValueChanged.AddListener(Change);
        ShowKey();

        this.gameObject.GetComponent<Button>().onClick.AddListener(ResetCurrentKey);
        UpButton.onClick.AddListener(ClickButtonUp);
        DownButton.onClick.AddListener(ClickButtonDown);
        LeftButton.onClick.AddListener(ClickButtonLeft);
        RightButton.onClick.AddListener(ClickButtonRight);
        RunButton.onClick.AddListener(ClickButtonRun);
        JumpButton.onClick.AddListener(ClickButtonJump);
        InteractButton.onClick.AddListener(ClickButtonInteract);
        AttackButton.onClick.AddListener(ClickButtonAttack);
        ResetButton.onClick.AddListener(ClickButtonReset);
    }

    // Update is called once per frame
    void Update ()
    {
    }

    public void Change(float arg0)
    {
        param.Sensi = Sensi.value;
        param.Save();
    }

    void ClickButtonUp()
    {
        currentkey = Up.name;
        UpText.text = "Selected";
    }

    void ClickButtonDown()
    {
        currentkey = Down.name;
        DownText.text = "Selected";
    }

    void ClickButtonLeft()
    {
        currentkey = Left.name;
        LeftText.text = "Selected";
    }

    void ClickButtonRight()
    {
        currentkey = Right.name;
        RightText.text = "Selected";
    }

    void ClickButtonRun()
    {
        currentkey = Run.name;
        RunText.text = "Selected";
    }
    void ClickButtonJump()
    {
        currentkey = Jump.name;
        JumpText.text = "Selected";
    }

    void ClickButtonInteract()
    {
        currentkey = Interact.name;
        InteractText.text = "Selected";
    }

    void ClickButtonAttack()
    {
        currentkey = Attack.name;
        AttackText.text = "Selected";
    }

    void ClickButtonReset()
    {
        param.Key = new Parametre().Key;
        param.Save();
        ShowKey();
    }

    void ResetCurrentKey()
    {
        ShowKey();
        currentkey = null;
    }

    void ChangeKey(string name, KeyCode key)
    {
        if (name != null)
        {
            param.Key[name] = key;
            ShowKey();
            param.Save();
        }
    }

    void ShowKey()
    {
        UpText.text = param.Key["MoveUp"].ToString();
        DownText.text = param.Key["MoveDown"].ToString();
        LeftText.text = param.Key["MoveLeft"].ToString();
        RightText.text = param.Key["MoveRight"].ToString();
        RunText.text = param.Key["Run"].ToString();
        JumpText.text = param.Key["Jump"].ToString();
        InteractText.text = param.Key["Interact"].ToString();
        AttackText.text = param.Key["Attack"].ToString();
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e != null)
        {
            if (e.shift)
                ChangeKey(currentkey, KeyCode.LeftShift);
                Debug.Log(currentkey + " : " + KeyCode.LeftShift.ToString());
            if (e.isKey)
                ChangeKey(currentkey, e.keyCode);
                Debug.Log(currentkey + " : " + e.keyCode.ToString());
        }
    }
}
