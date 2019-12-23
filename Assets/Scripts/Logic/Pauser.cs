using UnityEngine;
using UnityEngine.UI;
public class Pauser : MonoBehaviour
{
    public GameObject aplication;
    public GameObject menu;
    public GameObject pause_window;
    public GameObject end_window;
    public GameObject souds_manager;
    private bool pause = false; // Включена ли пауза
    private bool enable = true; // Отслеживание нажатия Esc
    // Update is called once per frame
    void Update()
    {
        if ((enable)&&(Input.GetButtonDown("Cancel")))
            if(pause == false)
                Pause_on();
            else
                Pause_off();
    }
    public void Pause_on(){ // Пауза вкючается
        pause = true;
        Cursor.visible = true;
        Time.timeScale = 0;
        souds_manager.GetComponent<AudioListener>().enabled = false;
        pause_window.SetActive(true);
    }
    public void Pause_off(){ // Пауза выкючается
        pause = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        souds_manager.GetComponent<AudioListener>().enabled = true;
        pause_window.SetActive(false);
    }
    public void Return_main_menu(){  // Возврат в главное меню
        pause = false;
        souds_manager.GetComponent<AudioListener>().enabled = true;
        Time.timeScale = 1;
        GameObject.Find("logic").GetComponent<Generator>().ClearAll();
        pause_window.SetActive(false);
        end_window.SetActive(false);
        menu.SetActive(true);
        enable = true;
        GameObject.Find("UI_chets").gameObject.GetComponent<Text>().text = "";
        aplication.SetActive(false);
    }
    public void The_end(){ // Вызов экрана смерти
        enable = false;
        Time.timeScale = 0;
        end_window.SetActive(true);
        Cursor.visible = true;
        GameObject.Find("Score_txt").GetComponent<Text>().text = "Ваш счет: " + GameObject.Find("logic").GetComponent<Score_manager>().score;
    }
}
