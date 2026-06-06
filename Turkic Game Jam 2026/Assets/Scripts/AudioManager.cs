using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource music;
    public AudioSource sfx;

    public AudioClip waterSplash;
    public AudioClip ingredientAdded;   
    public AudioClip correctChoice; 

    void Awake()
    {
        Instance = this;
    }

    public void PlaySfx(AudioClip clip)
    {
        sfx.pitch = Random.Range(0.8f, 1.2f);
        sfx.PlayOneShot(clip);
    }
}
