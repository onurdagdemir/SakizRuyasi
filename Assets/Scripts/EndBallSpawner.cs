using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBallSpawner : MonoBehaviour
{

    public GameObject[] ballPrefabs;
    public float spawnInterval = 2f; // Yaratma aralýðý (saniye)
    public List<GameObject> ballPrefabsList;

    void Start()
    {
        PlayerPrefs.SetString("LastStage", "GameEnd");

        if (PlayerPrefs.GetInt("StageCount") < 5)
        {
            PlayerPrefs.SetInt("StageCount", 5);
        }

        StartCoroutine(StartDialog());

        // Belirli süre aralýklarýnda SpawnBall fonksiyonunu çaðýr
        InvokeRepeating("SpawnBall", 1f, spawnInterval);

    }

    private IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(1f);
        StandardUI.Instance.Mission("Oyun Tamamlandý!", 3f, true);
        StandardUI.Instance.Dialog("Tebrikler, artýk sakýz makinesinin sana ihtiyacý kalmadý..", 4f, false);
        yield return new WaitForSeconds(5f);
        StandardUI.Instance.Dialog("Kim bilir, belki büyük bir sakýz makinesinin içinde yaþýyoruz..", 5f, false);
    }

    void SpawnBall()
    {

        if (ballPrefabsList.Count > 1)
        {
            Destroy(ballPrefabsList[0]);
            ballPrefabsList.RemoveAt(0);
        }

        // Rastgele bir top prefab'ý seç
        GameObject selectedBallPrefab = ballPrefabs[Random.Range(0, ballPrefabs.Length)];

        // Rastgele bir ölçek deðeri belirle
        float randomScale = Random.Range(0.5f, 1f);

        // Top prefab'ýný yarat
        GameObject newBall = Instantiate(selectedBallPrefab, new Vector3(0f, 1.4f, -1.5f), Quaternion.identity);

        // Topun ölçek deðerini belirlenen rastgele deðere ayarla
        newBall.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        ballPrefabsList.Add(newBall);

    }
}
