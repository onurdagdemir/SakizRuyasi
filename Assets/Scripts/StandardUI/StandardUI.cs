using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StandardUI : MonoBehaviour
{
    public static StandardUI Instance;

    public GameObject MissionBox;
    public GameObject InfoBox;
    public GameObject DialogBox;
    public TextMeshProUGUI MissionTxt;
    public TextMeshProUGUI InfoTxt;
    public TextMeshProUGUI DialogTxt;

    private float lastESCTime = 0f;
    private int escCount = 0;
    string currentSceneName;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        MissionBox = transform.Find("MissionBox").gameObject;
        InfoBox = transform.Find("InfoBox").gameObject;
        DialogBox = transform.Find("DialogBox").gameObject;
        MissionTxt = transform.Find("MissionBox/Text").GetComponent<TextMeshProUGUI>();
        InfoTxt = transform.Find("InfoBox/Text").GetComponent<TextMeshProUGUI>();
        DialogTxt = transform.Find("DialogBox/Text").GetComponent<TextMeshProUGUI>();

        MissionBox.SetActive(false);
        InfoBox.SetActive(false);
        DialogBox.SetActive(false);
        MissionTxt.text = "";
        InfoTxt.text = "";
        DialogTxt.text = "";

        currentSceneName = SceneManager.GetActiveScene().name;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
    }

    private void Update()
    {
        if (currentSceneName != "Menu")
        {
            if (Input.GetKeyDown(KeyCode.Escape) && escCount == 0)
            {
                Info("Çýkmak için ESC tuþuna basýn", 4f, false);
                lastESCTime = Time.time;
                escCount = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && lastESCTime + 4f >= Time.time)
            {
                escCount = 0;
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            }
            if (lastESCTime + 4f <= Time.time && escCount > 0)
            {
                escCount = 0;
            }
        }

    }

    public void Mission(string text, float duration, bool isConstant)
    {
        MissionTxt.text = text;
        MissionBox.SetActive(true);
        if (!isConstant)
        {
            StartCoroutine(MissionTextControl(duration));
        }
    }

    private IEnumerator MissionTextControl(float duration)
    {
        yield return new WaitForSeconds(duration);
        MissionBox.SetActive(false);
        MissionTxt.text = "";
    }

    public void Info(string text, float duration, bool isConstant)
    {
        InfoTxt.text = text;
        InfoBox.SetActive(true);
        if (!isConstant)
        {
            StartCoroutine(InfoTextControl(duration));
        }
    }

    private IEnumerator InfoTextControl(float duration)
    {
        yield return new WaitForSeconds(duration);
        InfoBox.SetActive(false);
        InfoTxt.text = "";
    }

    public void Dialog(string text, float duration, bool isConstant)
    {
        DialogTxt.text = text;
        DialogBox.SetActive(true);
        if (!isConstant)
        {
            StartCoroutine(DialogTextControl(duration));
        }
    }

    private IEnumerator DialogTextControl(float duration)
    {
        yield return new WaitForSeconds(duration);
        DialogBox.SetActive(false);
        DialogTxt.text = "";
    }

    public void CloseMission()
    {
        MissionBox.SetActive(false);
        MissionTxt.text = "";
    }
    public void CloseInfo()
    {
        InfoBox.SetActive(false);
        InfoTxt.text = "";
    }
    public void CloseDialog()
    {
        DialogBox.SetActive(false);
        DialogTxt.text = "";
    }
    public void CloseAll()
    {
        MissionBox.SetActive(false);
        MissionTxt.text = "";
        InfoBox.SetActive(false);
        InfoTxt.text = "";
        DialogBox.SetActive(false);
        DialogTxt.text = "";
    }


}
