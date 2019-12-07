using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds_manager : MonoBehaviour
{
    public Main SoundsSetting;      // Настройки громкости
    public AudioSource main_music;  // Главный трэк игры
    public AudioSource boss_music;  // Трэк босса
    public List<AudioSource> audio_sources; // Проигрываемые звуки (Плэйлист)
    private AudioSource aud; // новый звук
    public void Change_music_vol(){
        main_music.volume = SoundsSetting.val;
        boss_music.volume = SoundsSetting.val;
    }
    public void Change_music_vol(float volumea){
        main_music.volume = volumea;
        boss_music.volume = volumea;
    }
    public void Boss_track(bool action){
        if (action){
            main_music.Stop();
            boss_music.Play();
        }
        else{
            boss_music.Stop();
            main_music.Play();
        }
    }
    private void Update() { 
        if (audio_sources.Count != 0)               // Если плейлист не пуст, то 
        {
            foreach (var audio in audio_sources)    // Для каждого звука
            {
                if (!audio.isPlaying){              // Если он "проиграл"(закойчил своё воспроизведение)
                    Destroy(audio);                 // Удаляем его AudioSource
                    audio_sources.Remove(audio);    // Удаляем его из плэйлиста
                    break;
                }
            }
        }
    }
    public void Play_request(AudioClip clip_chik) // Метод принимает звук и воспроизводит его
    {
        aud = gameObject.AddComponent<AudioSource>();    // Создаём новый AudioSource
        aud.clip = clip_chik;                            // Добавляем в него звук
        aud.volume = SoundsSetting.val;                  // Настраиваем громкость звука
        aud.Play();                                      // Проигрываем
        audio_sources.Add(aud);                          // Добавляем в плэйлист
    }
    public void Play_request(AudioClip clip_chik, float vol)// Метод принимает звук и воспроизводит его с указанной громкостью
    {
        aud = gameObject.AddComponent<AudioSource>();       // Создаём новый AudioSource
        aud.clip = clip_chik;                               // Добавляем в него звук
        aud.volume = SoundsSetting.val * vol;               // Настраиваем громкость звука 
        aud.Play();                                         // Проигрываем
        audio_sources.Add(aud);                             // Добавляем в плэйлист
    }
}
