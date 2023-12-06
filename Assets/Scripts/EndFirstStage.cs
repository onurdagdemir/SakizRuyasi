using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class EndFirstStage : MonoBehaviour
{
    public delegate void EndGameEventHandler();
    public static event EndGameEventHandler OnEndFirstStage;
    public static event EndGameEventHandler OnEndFirstGame;

    public GameObject Camera;
    public Camera PlayerCamera;
    public LayerMask gearLayer;
    public GameObject Gears;
    public GameObject Gear1;
    public GameObject Gear2;
    public GameObject OtherParts;

    private Vector3 targetPos;
    bool isFinish = false;
    bool canLook = false;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !isFinish)
        {
            OnEndFirstGame();
            StandardUI.Instance.Mission("Görev Baþarýlý", 3f, true);
            StandardUI.Instance.Dialog("Makine- Tebrikler, görevin için hazýr mýsýn?", 3f, false);
            isFinish = true;
            StartCoroutine(EndAnimation());
        }
    }

    private IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(2f);
        OnEndFirstStage();
        Gears.SetActive(true);
        Camera.GetComponent<Rigidbody>().isKinematic = true;
        Camera.transform.DOLookAt(Gears.transform.position, 1.5f);
        yield return new WaitForSeconds(1.5f);
        targetPos = Gears.transform.position + Vector3.down * 0.3f + Vector3.right * 0.1f;
        canLook = true;
        Camera.transform.DOMove(new Vector3(11.85f, 1, -21.57f), 2f);
        yield return new WaitForSeconds(2.5f);
        Camera.transform.DOMove(targetPos, 35f);
        Gears.transform.DOMove(new Vector3(11f, 1.67f, -22f), 5f);
        Gears.transform.DORotate(new Vector3(-6f, 65f, 1.3f), 5f);
        yield return new WaitForSeconds(1f);
        StandardUI.Instance.CloseAll();
        SetCameraFocus();
        OtherParts.SetActive(false);
        yield return new WaitForSeconds(4f);
        Gear1.transform.DOMove(new Vector3(11f, 1.67f, -22f), 3f);
        yield return new WaitForSeconds(3f);
        Gear1.transform.DORotate(new Vector3(0f, 0f, 60f), 10f, RotateMode.LocalAxisAdd);
        Gear2.transform.DORotate(new Vector3(0f, 0f, -75f), 10f, RotateMode.LocalAxisAdd);
        yield return new WaitForSeconds(3f);
        DOTween.KillAll();
        StartCoroutine(NextScene());
    }

    private void Update()
    {
        if (canLook)
        {
            Camera.transform.LookAt(targetPos);
        }
    }

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Game1-2");
    }

    void SetCameraFocus()
    {
        // Sadece gearLayer'ý göster
        PlayerCamera.cullingMask = gearLayer;

        // Arka plan rengini siyah yap
        PlayerCamera.clearFlags = CameraClearFlags.SolidColor;
        PlayerCamera.backgroundColor = Color.black;
    }


}
