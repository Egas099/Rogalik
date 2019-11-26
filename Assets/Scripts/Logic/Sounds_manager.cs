using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds_manager : MonoBehaviour
{
    public AudioSource main_music;           //
    public AudioSource boss_music;
    public AudioSource bite;            // Поедание
    public AudioSource take_coin;       //
    public AudioSource take_heard;      //
    public AudioSource take_key;        //
    public AudioSource level_up;
    public AudioSource other;

    private void Start() {
    }
    public void Boss_track(bool action){
        if (action)
        {
            main_music.Stop();
            boss_music.Play();
        }
        else
        {
            boss_music.Stop();
            main_music.Play();
        }
    }
    public void Play_pls(AudioClip clip_chik)
    {
        other.clip = clip_chik;
        other.Play();
    }
}
