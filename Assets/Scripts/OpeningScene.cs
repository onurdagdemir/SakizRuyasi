using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScene : MonoBehaviour
{
    public GameObject PostProcess;
    private Animator _postProcessAnm;
    private void Start()
    {
        Animator animator = GetComponent<Animator>();
        _postProcessAnm = PostProcess.GetComponent<Animator>();
        animator.SetTrigger("OpeningScene");
        PlayerPrefs.SetString("LastStage", "OpeningScene");

        if (PlayerPrefs.GetInt("StageCount") < 1)
        {
            PlayerPrefs.SetInt("StageCount", 1);
        }

        Cursor.lockState = CursorLockMode.Locked;
    }
    public void GoDark()
    {
        _postProcessAnm.SetTrigger("GoDark");
    }

    public void GoWhite()
    {
        _postProcessAnm.SetTrigger("GoWhite");
    }
    public void GoNextScene()
    {
        SceneManager.LoadScene("Game1");
    }

    public void Dialog(string dialog)
    {
        StandardUI.Instance.Dialog(dialog, 3f, false);
    }
}
