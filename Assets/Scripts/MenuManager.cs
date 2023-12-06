using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject StagesWindow;
    public Button ResumeButton;

    public Button Game1;
    public Button Game12;
    public Button Game2;
    public Button GameEnd;

    private string lastStage;
    private int canPlayStages;
    private bool isStagesOn = false;
    // Start is called before the first frame update
    void Start()
    {
        StandardUI.Instance.CloseAll();
        Cursor.lockState = CursorLockMode.Confined;
        StagesWindow.SetActive(false);
        canPlayStages = PlayerPrefs.GetInt("StageCount");

        StagesControl();

        if (PlayerPrefs.HasKey("LastStage"))
        {
            lastStage = PlayerPrefs.GetString("LastStage");
        }
        else
        {
            ResumeButton.interactable = false;
        }
    }

    public void ResumeGame()
    {
        SceneManager.LoadScene(lastStage);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("OpeningScene");
    }

    public void OpenCloseStages()
    {
        isStagesOn = !isStagesOn;
        StagesWindow.SetActive(isStagesOn);
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadGame(int Stage)
    {
        switch (Stage)
        {
            case 5:
                SceneManager.LoadScene("GameEnd");
                break;
            case 4:
                SceneManager.LoadScene("Game2");
                break;
            case 3:
                SceneManager.LoadScene("Game1-2");
                break;
            case 2:
                SceneManager.LoadScene("Game1");
                break;
        }
    }

    private void StagesControl()
    {
        switch (canPlayStages)
        {
            case 5:
                GameEnd.interactable = true;
                Game2.interactable = true;
                Game12.interactable = true;
                Game1.interactable = true;
                break;
            case 4:
                GameEnd.interactable = false;
                Game2.interactable = true;
                Game12.interactable = true;
                Game1.interactable = true;
                break;
            case 3:
                GameEnd.interactable = false;
                Game2.interactable = false;
                Game12.interactable = true;
                Game1.interactable = true;
                break;
            case 2:
                GameEnd.interactable = false;
                Game2.interactable = false;
                Game12.interactable = false;
                Game1.interactable = true;
                break;
            case 1:
                GameEnd.interactable = false;
                Game2.interactable = false;
                Game12.interactable = false;
                Game1.interactable = false;
                break;
            case 0:
                GameEnd.interactable = false;
                Game2.interactable = false;
                Game12.interactable = false;
                Game1.interactable = false;
                break;
        }
    }

}
