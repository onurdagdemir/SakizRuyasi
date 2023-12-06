using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GumballManager : MonoBehaviour
{
    public static GumballManager Instance;

    public TextMeshProUGUI GumballAmountTxt;

    private int gumballAmount = 0;

    public Action<bool> OnGumballAmountOK;

    public AudioClip gainSound;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GumballAmountTxt.text = "Sak�z Miktar�: 0";
    }

    public void AddGumball(int amount)
    {
        PlaySound(gainSound);
        gumballAmount += amount;
        GumballAmountTxt.text = "Sak�z Miktar�: " + gumballAmount;
        if(gumballAmount == 10)
        {
            OnGumballAmountOK?.Invoke(true);
        }
    }

    public void RemoveGumball()
    {
        gumballAmount = 0;
        GumballAmountTxt.text = "Sak�z Miktar�: " + gumballAmount;
    }

    public int GetGumballAmount()
    {
        return gumballAmount;
    }

    void PlaySound(AudioClip clip)
    {
        // Ses dosyas�n� �al
        audioSource.clip = clip;
        audioSource.Play();
    }
}
