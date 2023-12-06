using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;

    public AudioClip[] musicClips; // M�zik dosyalar�
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

        // �lk m�zi�i ba�lat
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
        // E�er m�zik tamamland�ysa, bir sonraki m�zi�i �al
        if (!audioSource.isPlaying)
        {
            PlayNextMusic();
        }
    }

    void PlayNextMusic()
    {
        // Bir sonraki m�zi�i se� ve �al
        AudioClip nextMusic = musicClips[Random.Range(0, musicClips.Length)];
        audioSource.clip = nextMusic;
        audioSource.Play();
    }

    void PlayMusic(int which)
    {
        // Bir sonraki m�zi�i se� ve �al
        AudioClip nextMusic = musicClips[which];
        audioSource.clip = nextMusic;
        audioSource.Play();
    }

}
