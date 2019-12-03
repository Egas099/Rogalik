using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Main : MonoBehaviour
{
    public float val = 1;
    private float save = 1;
    public Slider sl;
    public AudioSource vol;
    public AudioSource volClick;
    public Sounds_manager sounds_Manager;
    public void Start()
    {
        // sounds_Manager = GameObject.Find("SoundsManager").GetComponent<Sounds_manager>();
        val = PlayerPrefs.GetFloat("volume");
        sl.value = val;
        vol.volume = val;
        volClick.volume = val;
    }

    public void GameQuit() { Application.Quit(); }
 

    public void SliderEvents(Slider sl)
    {
        val = sl.value;
        sl.value = val;
        SaveChanges();
    }

    public void SaveChanges()
    {
       save = PlayerPrefs.GetFloat("volume");

        if (save != val)
        {
            PlayerPrefs.SetFloat("volume", val);

            
            vol.volume = val;
            volClick.volume = val;
            sounds_Manager.Change_music_vol();
        }
    }
}
