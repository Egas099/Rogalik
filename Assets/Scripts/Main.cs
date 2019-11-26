using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Main : MonoBehaviour
{

    private float val = 1;
    private float save = 1;
    public Slider sl;
    public AudioSource vol;
    public AudioSource volClick;

    public void Start()
    {
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
        }
    }
}
