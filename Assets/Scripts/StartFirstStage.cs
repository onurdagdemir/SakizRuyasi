using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFirstStage : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetString("LastStage", "Game1");

        if (PlayerPrefs.GetInt("StageCount") < 2)
        {
            PlayerPrefs.SetInt("StageCount", 2);
        }

        StartCoroutine(StartDialog());

    }

    private IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(1f);
        StandardUI.Instance.Mission("Görev: Sakýz makinesine git", 3f, true);
        StandardUI.Instance.Dialog("Burasý da ne böyle?", 3f, false);
        yield return new WaitForSeconds(4f);
        StandardUI.Instance.Dialog("Uyanmam lazým! Bu rüya olmalý..", 3f, false);
    }
}
