using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] soundClips;
    AudioSource audSrc;

    public static SoundManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        audSrc = GetComponent<AudioSource>();
    }

    public void PlaySound(int soundID, float vol = 1.0f)
    {
        audSrc.pitch = Random.Range(0.90f, 1.10f);
        audSrc.PlayOneShot(soundClips[soundID], vol);
    }
}
