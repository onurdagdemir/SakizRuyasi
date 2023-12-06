using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;

    public AudioClip[] musicClips; // Müzik dosyalarý
    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Ýlk müziði baþlat
        PlayMusic(0);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Menu":
                PlayMusic(0);
                break;
            case "OpeningScene":
                PlayMusic(3);
                break;
            case "Game1":
                PlayMusic(2);
                break;
            case "Game1-2":
                PlayMusic(3);
                break;
            case "Game2":
                PlayMusic(1);
                break;
            case "GameEnd":
                PlayMusic(4);
                break;
        }
    }

    void Update()
    {
        // Eðer müzik tamamlandýysa, bir sonraki müziði çal
        if (!audioSource.isPlaying)
        {
            PlayNextMusic();
        }
    }

    void PlayNextMusic()
    {
        // Bir sonraki müziði seç ve çal
        AudioClip nextMusic = musicClips[Random.Range(0, musicClips.Length)];
        audioSource.clip = nextMusic;
        audioSource.Play();
    }

    void PlayMusic(int which)
    {
        // Bir sonraki müziði seç ve çal
        AudioClip nextMusic = musicClips[which];
        audioSource.clip = nextMusic;
        audioSource.Play();
    }

}
