using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_manager : MonoBehaviour
{
    [HideInInspector]
    public float level;// Текущий уровень
    public Text text_number_keys;
    public int number_keys = 0;// количество ключей
    const int max_number_keys = 100; // максимальное количество ключей
    public Text text_score; // Отображение счета
    public float score = 0; // счет
    const float max_score = 999999; // максимальный счет
    private float view_score = 0; // отображаемый счет
    private bool run = false; // состояние увеличения счета
    public Highscores scores;

    private int raznica; 
    // Start is called before the first frame update
    void Start(){
        level = 0;
        // text_number_keys = GameObject.Find("Text_key").gameObject.GetComponent<Text>();
        // text_score = GameObject.Find("UI_score").gameObject.GetComponent<Text>();
        Show_score();
    }
    void Show_score(){
        text_score.text = "Cчёт: " + view_score;
    }
    void FixedUpdate() {
        if (run)
        {
            if (view_score < score)
            {
                text_score.fontSize = 84;
                view_score += raznica;
                Show_score();
                return;
            }
            view_score = score;
            run = false;
            text_score.fontSize = 80;
            Show_score();
        }
    }
    public void Add_score(int how_much){
        try{score += (how_much * (1 + level/10));}
        catch{score = max_score;}
        finally{
            if (score >= max_score)
                score = max_score;
        }
        run = true;
        raznica = Mathf.RoundToInt((score - view_score)/10);
    }
    public void Clear_scr(){
        scores.New_score(score);
        score = 0;
        view_score = 0;
        run = false;
        Start();
    }
    public void Add_key(){
        number_keys++;
        if (number_keys > max_number_keys)
            number_keys = max_number_keys;
        text_number_keys.text = "x" + number_keys;
    }
    public bool Remove_key(){
        if (number_keys > 0)
        {
            number_keys--;
            text_number_keys.text = "x" + number_keys;

            return true;
        }
        else
            return false;
    }
    public void Clear_keys(){
        number_keys = 0;
        text_number_keys.text = "x" + number_keys;
    }
}
