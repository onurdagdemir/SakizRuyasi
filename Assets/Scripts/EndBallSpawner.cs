using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBallSpawner : MonoBehaviour
{

    public GameObject[] ballPrefabs;
    public float spawnInterval = 2f; // Yaratma aral��� (saniye)
    public List<GameObject> ballPrefabsList;

    void Start()
    {
        PlayerPrefs.SetString("LastStage", "GameEnd");

        if (PlayerPrefs.GetInt("StageCount") < 5)
        {
            PlayerPrefs.SetInt("StageCount", 5);
        }

        StartCoroutine(StartDialog());

        // Belirli s�re aral�klar�nda SpawnBall fonksiyonunu �a��r
        InvokeRepeating("SpawnBall", 1f, spawnInterval);

    }

    private IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(1f);
        StandardUI.Instance.Mission("Oyun Tamamland�!", 3f, true);
        StandardUI.Instance.Dialog("Tebrikler, art�k sak�z makinesinin sana ihtiyac� kalmad�..", 4f, false);
        yield return new WaitForSeconds(5f);
        StandardUI.Instance.Dialog("Kim bilir, belki b�y�k bir sak�z makinesinin i�inde ya��yoruz..", 5f, false);
    }

    void SpawnBall()
    {

        if (ballPrefabsList.Count > 1)
        {
            Destroy(ballPrefabsList[0]);
            ballPrefabsList.RemoveAt(0);
        }

        // Rastgele bir top prefab'� se�
        GameObject selectedBallPrefab = ballPrefabs[Random.Range(0, ballPrefabs.Length)];

        // Rastgele bir �l�ek de�eri belirle
        float randomScale = Random.Range(0.5f, 1f);

        // Top prefab'�n� yarat
        GameObject newBall = Instantiate(selectedBallPrefab, new Vector3(0f, 1.4f, -1.5f), Quaternion.identity);

        // Topun �l�ek de�erini belirlenen rastgele de�ere ayarla
        newBall.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        ballPrefabsList.Add(newBall);

    }
}
