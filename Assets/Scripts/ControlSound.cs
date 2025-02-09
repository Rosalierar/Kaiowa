using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSound : MonoBehaviour
{
    public AudioClip[] effects;
    public AudioClip[] music;

    public AudioSource playMusic;
    public int whatMusic = 0;

    public AudioSource playEffects;
    public int whatEffects = 0;



    // Start is called before the first frame update
    void Start()
    {
        playMusic = GetComponent<AudioSource>();
        playEffects = GetComponent<AudioSource>();

        if (playMusic != null)
        {
            playMusic.clip = music[whatMusic];
            playMusic.Play();
        }
    }
    public void SoundOnOff(int tf)
    {
        if (whatMusic != tf)
        {
            Debug.Log(tf);
            whatMusic = tf;

            playMusic.Stop();
            playMusic.clip = music[whatMusic];
            playMusic.Play();
        }
    }
    public void PlayEffects(int num)
    {
        Debug.Log(num);
        whatEffects = num;

        playEffects.Stop();
        playEffects.clip = effects[whatEffects];
        playEffects.Play();
    }
}
