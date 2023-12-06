using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game12 : MonoBehaviour
{
    public Camera PlayerCamera;
    public GameObject GumballMachine;
    public LayerMask gearLayer;
    public GameObject Animation;

    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Game1-2");
        GumballMachine.SetActive(false);
        Animation.SetActive(false);
        PlayerPrefs.SetString("LastStage", "Game1-2");

        if (PlayerPrefs.GetInt("StageCount") < 3)
        {
            PlayerPrefs.SetInt("StageCount", 3);
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Dialog(string dialog)
    {
        StandardUI.Instance.Dialog(dialog, 3f, false);
    }

    public void GoNextScene()
    {
        SceneManager.LoadScene("Game2");
    }

    public void SetCameraFocus()
    {
        // Sadece gearLayer'ý göster
        PlayerCamera.cullingMask = gearLayer;

        // Arka plan rengini siyah yap
        PlayerCamera.clearFlags = CameraClearFlags.SolidColor;
        PlayerCamera.backgroundColor = Color.black;
        StartCoroutine(EndAnimation());
    }

    private IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(1f);
        GumballMachine.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GumballMachine.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        GumballMachine.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GumballMachine.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        GumballMachine.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Dialog("Makine- Sakýza ihtiyacým var!!");
        yield return new WaitForSeconds(0.5f);
        Animator gumballAnimator = GumballMachine.GetComponent<Animator>();
        Animation.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gumballAnimator.SetTrigger("GumBallAnm");


        yield return new WaitForSeconds(5f);
        GoNextScene();
    }

}
