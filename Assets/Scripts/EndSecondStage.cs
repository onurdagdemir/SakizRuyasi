using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class EndSecondStage : MonoBehaviour
{
    public delegate void EndSecondGameEventHandler();
    public static event EndSecondGameEventHandler OnEndSecondStage;

    public GameObject GumballsObject;
    public GameObject LightEffect;


    public GameObject Camera;
    public Camera PlayerCamera;
    public LayerMask gearLayer;
    public GameObject Gears;
    public GameObject Gear1;
    public GameObject Gear2;
    public GameObject Gear3;
    public GameObject OtherParts;

    private bool isFinish = false;
    private bool isAnmStart = false;

    // Start is called before the first frame update
    void Start()
    {
        GumballManager.Instance.OnGumballAmountOK += ToDoEndOfGame;
        GumballsObject.SetActive(false);
        LightEffect.SetActive(false);

        PlayerPrefs.SetString("LastStage", "Game2");

        if (PlayerPrefs.GetInt("StageCount") < 4)
        {
            PlayerPrefs.SetInt("StageCount", 4);
        }

        StartCoroutine(StartDialog());
    }

    private IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(1f);
        StandardUI.Instance.Mission("Görev: 10 Adet Sakýz Topla", 3f, true);
        StandardUI.Instance.Dialog("Makine- Müþterilerimin acil sakýza ihtiyacý var!", 3f, false);
        yield return new WaitForSeconds(4f);
        StandardUI.Instance.Dialog("Makine- Sadece kýrmýzý, mavi ve sarý sakýzlar istiyorum", 3f, false);
        yield return new WaitForSeconds(6f);
        StandardUI.Instance.Dialog("Makine- Zaten kimse o ekþi yeþil sakýzlarý sevmez..", 3f, false);
        yield return new WaitForSeconds(6f);
        StandardUI.Instance.Dialog("Makine- Öyle koca koca sakýz istemem, biraz ufak olsun!", 3f, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isFinish && !isAnmStart)
        {
            StandardUI.Instance.Mission("Görev Baþarýlý", 3f, true);
            StandardUI.Instance.Dialog("Makine- Sakýzlarýmý bana ver!", 3f, false);
            StartCoroutine(EndAnimation());
            isAnmStart = true;
        }
    }

    private IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(1f);
        GumballsObject.SetActive(true);
        GumballManager.Instance.RemoveGumball();
        yield return new WaitForSeconds(2f);
        OnEndSecondStage();
        Gears.SetActive(true);
        Camera.GetComponent<Rigidbody>().isKinematic = true;

            Vector3[] path = new Vector3[]
        {
            new Vector3(0f, 1f, 19f),
            new Vector3(0f, 5.05f, 21f),
            new Vector3(0f, 5.05f, 23.5f)
        };

        // Yolu ve hedefi kullanarak tween'i oluþtur
        Camera.transform.DOPath(path, 10f, PathType.CatmullRom, PathMode.Full3D)
            .SetOptions(false, AxisConstraint.None, AxisConstraint.Z)
            .SetLookAt(new Vector3(0f, 5.1f, 24.81f), true); // Hedefe bak

        yield return new WaitForSeconds(6.5f);

        StandardUI.Instance.Dialog("Her þey tamam artýk..", 3f, false);

        yield return new WaitForSeconds(2f);

        Gears.transform.DOMove(new Vector3(0f, 6.1f, 24.9f), 5f);
        Gears.transform.DORotate(new Vector3(0f, -20f, 0f), 5f);
        yield return new WaitForSeconds(1f);
        StandardUI.Instance.CloseAll();
        SetCameraFocus();
        OtherParts.SetActive(false);
        yield return new WaitForSeconds(4f);
        Gear3.transform.DOMove(new Vector3(0f, 6.1f, 24.9f), 3f);
        yield return new WaitForSeconds(3f);
        Gear1.transform.DORotate(new Vector3(0f, 0f, -45f), 10f, RotateMode.LocalAxisAdd);
        Gear2.transform.DORotate(new Vector3(0f, 0f, 60f), 10f, RotateMode.LocalAxisAdd);
        Gear3.transform.DORotate(new Vector3(0f, 0f, 60f), 10f, RotateMode.LocalAxisAdd);
        yield return new WaitForSeconds(3f);
        DOTween.KillAll();
        StartCoroutine(NextScene());
    }

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameEnd");
    }

    void SetCameraFocus()
    {
        // Sadece gearLayer'ý göster
        PlayerCamera.cullingMask = gearLayer;

        // Arka plan rengini siyah yap
        PlayerCamera.clearFlags = CameraClearFlags.SolidColor;
        PlayerCamera.backgroundColor = Color.black;
    }

    private void ToDoEndOfGame(bool isAmountReached)
    {
        if(isAmountReached)
        {
            isFinish = true;
            LightEffect.SetActive(true);
            StandardUI.Instance.Mission("Görev: Topladýðýn sakýzlarý makineye götür", 3f, true);
            StandardUI.Instance.Dialog("Makine- Haydi, sakýzlar nerede kaldý?", 3f, false);
        }
    }
}
