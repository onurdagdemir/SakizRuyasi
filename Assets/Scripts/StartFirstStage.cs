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
        StandardUI.Instance.Mission("G�rev: Sak�z makinesine git", 3f, true);
        StandardUI.Instance.Dialog("Buras� da ne b�yle?", 3f, false);
        yield return new WaitForSeconds(4f);
        StandardUI.Instance.Dialog("Uyanmam laz�m! Bu r�ya olmal�..", 3f, false);
    }
}
