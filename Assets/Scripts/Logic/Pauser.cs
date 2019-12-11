using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pauser : MonoBehaviour
{
    public GameObject aplication;
    public GameObject menu;
    public GameObject pause_window;
    public GameObject end_window;
    public GameObject souds_manager;
    private bool pause = false;
    private bool enable = true;
    // Update is called once per frame
    void Update()
    {
        if ((enable)&&(Input.GetButtonDown("Cancel")))
            if(pause == false)
                Pause_on();
            else
                Pause_off();
    }
    public void Pause_on(){
        pause = true;
        Time.timeScale = 0;
        souds_manager.GetComponent<AudioListener>().enabled = false;
        pause_window.SetActive(true);
    }
    public void Pause_off(){
        Cursor.visible = false;
        pause = false;
        Time.timeScale = 1;
        souds_manager.GetComponent<AudioListener>().enabled = true;
        pause_window.SetActive(false);
    }
    public void Return_main_menu(){
        pause = false;
        souds_manager.GetComponent<AudioListener>().enabled = true;
        Time.timeScale = 1;
        GameObject.Find("logic").GetComponent<Generator>().ClearAll();
        pause_window.SetActive(false);
        end_window.SetActive(false);
        menu.SetActive(true);
        enable = true;
        aplication.SetActive(false);
    }
    public void The_end(){
        enable = false;
        Time.timeScale = 0;
        end_window.SetActive(true);
        Cursor.visible = true;
        GameObject.Find("Score_txt").GetComponent<Text>().text = "Ваш счет: " + GameObject.Find("logic").GetComponent<Score_manager>().score;
    }
}
