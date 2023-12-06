using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class PushButton : MonoBehaviour
{
    public Transform Button;
    private int objectCounter = 0;
    private bool isAction = false;
    private bool isPressed = false;
    private bool isGameEnd = false;
    private int tryButtonCount = 0;

    private float objectOnTheButton = 0f;

    public GameObject FinishLevel;
    public GameObject FinishLevelImage;


    private void Start()
    {
        FinishLevel.SetActive(false);
        EndFirstStage.OnEndFirstGame += EndGame;
    }

    private void EndGame()
    {
        isGameEnd = true;
    }

    private void OnDestroy()
    {
        EndFirstStage.OnEndFirstGame -= EndGame;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("Targetable"))
        {
            if (objectCounter == 0)
            {
                if(!isAction)
                {
                    StartCoroutine(ReleaseMove());
                    StartCoroutine(MoveButton(true, 0.2f));
                }

            }
            objectCounter++;
            tryButtonCount++;
            if (tryButtonCount == 10)
            {
                Help();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("Targetable"))
        {
            StartCoroutine(MoveButton(true, 0.2f));
            objectOnTheButton = Time.time;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("Targetable"))
        {
            if (!isAction)
            {
                StartCoroutine(ReleaseMove());
                StartCoroutine(MoveButton(false, 0.2f));
            }
            objectCounter--;
        }
    }

    private void Update()
    {
        if (objectOnTheButton + 0.3f <= Time.time)
        {
            StartCoroutine(MoveButton(false, 0.2f));
        }
    }

    private IEnumerator ReleaseMove()
    {
        isAction = true;
        yield return new WaitForSeconds(0.2f);
        isAction = false;
    }

        private IEnumerator MoveButton(bool isPush, float duration)
    {
        if (isPush && !isPressed)
        {
            Button.transform.DOMoveY(0.02f, duration);
            isPressed = true;
        }
        else if (!isPush && isPressed)
        {
            Button.transform.DOMoveY(0.1f, duration);
            isPressed = false;
        }
        yield return new WaitForSeconds(duration/2);
        if (isPush)
        {
            FinishLevel.SetActive(true);
        }
        else if(!isGameEnd)
        {
            FinishLevel.SetActive(false);
        }

    }

    private void Help()
    {
        StandardUI.Instance.Info("Butonun üzerine küpü koyabilirsin..", 6f, false);
        FinishLevelImage.SetActive(true);
    }
}
