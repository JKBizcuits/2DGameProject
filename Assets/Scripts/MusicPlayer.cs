using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] playlistNormal;
    public AudioClip[] playlistChased;
    public AudioClip[] playlistDead;
    private AudioSource audio;
    public bool chased;
    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.loop = false;
    }



    

    // Update is called once per frame
    void Update()
    {

        
        if (chased)
        {
            if (!audio.isPlaying)
            {
                //audio.Stop();
                audio.clip = playlistChased[Random.Range(0, playlistChased.Length)];
                audio.Play();
            }
        }
        else if(dead)
        {
            if (!audio.isPlaying)
            {
                //audio.Stop();
                audio.clip = playlistDead[Random.Range(0, playlistDead.Length)];
                audio.Play();
            }
        }
        else
        {
            if (!audio.isPlaying)
            {
                audio.clip = playlistNormal[Random.Range(0, playlistNormal.Length)];
                audio.Play();
            }

            
        }
    }
}
