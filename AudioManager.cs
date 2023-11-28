using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private AudioSource _audioSource;
    [SerializeField] private AudioClip hitAudioClip;
    [SerializeField] private AudioClip missAudioClip;

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
        _audioSource = GetComponent<AudioSource>();
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    */

    public void PlayHitAudio()
    {
        _audioSource.PlayOneShot(hitAudioClip);
    }
    
    public void PlayMissAudio()
    {
        _audioSource.PlayOneShot(missAudioClip);
    }

}
