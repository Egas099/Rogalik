using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_manager : MonoBehaviour
{
    [HideInInspector]
    public float level;
    public Text Text_out;
    public float score = 0;
    private float view_score = 0;
    private bool run = false;
    // Start is called before the first frame update
    void Start(){
        Show_score();
        level = 0;
    }
    void Show_score(){
        Text_out.text = "Cчёт: " + view_score;
    }
    void FixedUpdate() {
        if (run)
        {
            if (view_score < score)
            {
                Text_out.fontSize = 82;
                view_score += 1;
                Show_score();
                return;
            }
            run = false;
            Text_out.fontSize = 80;
        }
    }
    public void Add_score(int how_much){
        score += (how_much * (1 + level/10));
        run = true;
    }
    public void Clear_scr(){
        score = 0;
        view_score = 0;
        run = false;
        Start();
    }
}
