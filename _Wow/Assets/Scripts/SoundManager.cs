using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip teleport, walk, jump, coin;
    static AudioSource _audioSource;

    public AudioClip[] clips;
    

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //Background
        _audioSource.clip = clips[Random.Range(0, clips.Length)]; //Background
        //OneShot
        _audioSource.loop = false;
        teleport = Resources.Load<AudioClip>("Blah");
        walk = Resources.Load<AudioClip>("foot");
        jump = Resources.Load<AudioClip>("Jump");
        coin = Resources.Load<AudioClip>("Put");

        _audioSource.PlayOneShot(_audioSource.clip);
    }
    public static void PlaySound(string clip, bool switchbutton = true)
    {
        switch (clip)
        {
            case "Blah":
                _audioSource.PlayOneShot(teleport);
                switchbutton = false;
                break;
            case "foot":
                _audioSource.PlayOneShot(walk);
                switchbutton = false;
                break;
            case "Jump":
                _audioSource.PlayOneShot(jump);
                switchbutton = false;
                break;
            case "Put":
                _audioSource.PlayOneShot(coin);
                switchbutton = false;
                break;
        }
    }
}