using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscores : MonoBehaviour
{
    public Text[] text_ui;
    public float[] scores;
    // Start is called before the first frame update
    public void Start()
    {
        Extract();
        Display();
    }

    void Extract() // Извлечение рекордов
    {
        for (int i = 0; i < 3; i++)
            scores[i] = PlayerPrefs.GetFloat("score" + (i+1));
    }
    public void Clear_scores() // Очистка (обнуление) рекордов
    {
        for (int i = 0; i < 3; i++){
            PlayerPrefs.SetFloat("score" + (i+1), 0f);
            scores[i] = 0;
        }
        Display();
    }
    public void Display() // Отображение
    {
        for (int i = 0; i < 3; i++)
            text_ui[i].text = scores[i].ToString();
    }
    public void Record() // Запись
    {
        for (int i = 0; i < 3; i++)
            PlayerPrefs.SetFloat("score" + (i+1), scores[i]);
    }
    public void New_score(float score)
    {
        Extract();
        float k = 0;
        for (int i = 0; i < 3; i++)
        {
            if (scores[i] < score)
            {
                k = scores[i];
                scores[i] = score;
                score = k;
            }
        }
        Record();
    }
}
