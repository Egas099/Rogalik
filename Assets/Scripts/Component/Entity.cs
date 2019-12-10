using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    
    public bool enable = true;
    public float max_health;
    public float health;
    public float damage;
    public float attack_speed;
    public float melee_attack_speed;
    public float speed;
    [HideInInspector]
    public int number_keys = 0;
    [HideInInspector]
    public Attack_system attack_system;

    public AudioClip audio_greeting;        // Приветствие
    public AudioClip audio_move;        // Перемещение
    public AudioClip audio_health_down;     // Получение урона
    public AudioClip audio_range_attack;    // Выстрел
    public AudioClip audio_melee_attack;    // Удар
    public AudioClip audio_die;

    public GameObject die_obj;
    private UI_hearts hearts_UI;
    [HideInInspector]
    public Sounds_manager sounds_Manager;
    void Start()
    {
        hearts_UI = GameObject.Find("UI_heart").gameObject.GetComponent<UI_hearts>();
        sounds_Manager = GameObject.Find("SoundsManager").GetComponent<Sounds_manager>();
        attack_system = GetComponent<Attack_system>();
        if(gameObject.tag == "Player")
            hearts_UI.Change_hearts(health);
    }
    void FixedUpdate()
    {
        if(enable)
            Check_health();
    }
    void Check_health()
    {
        if (health <= 0)
            Die();      
    }
    public void Stat_changed(string what, float how_much){ // Метод изменяющий характеристики сущности
        switch (what)
        {
            case "health":
            if(how_much < 0)
            {
                if(audio_health_down != null)
                    sounds_Manager.Play_request(audio_health_down);
            }
            health += how_much;
            if(gameObject.tag == "Player")
                hearts_UI.Change_hearts(health);
            break;
            case "damage":
            damage += how_much;
            break;
            case "attack_speed":
            attack_speed += how_much;
            break;
            case "speed":
            speed += how_much;
            break;
            default:break;
        }
    }
    public void Enable_entity(){
        if(enable == false)
        {
            enable = true;
            if (audio_greeting!=null)
                sounds_Manager.Play_request(audio_greeting);
        }
    }
    void Die(){
        enable = false;
        if (gameObject.tag == "Monster")
            GameObject.Find("logic").GetComponent<Score_manager>().Add_score(30);
        if(audio_die!=null)
            sounds_Manager.Play_request(audio_die);
        if(gameObject.GetComponent<Drop>())
            gameObject.GetComponent<Drop>().Drop_(transform.position);
        if(die_obj)
            Instantiate(die_obj,transform.position,Quaternion.identity);
        if (gameObject.tag == "Player")
                GameObject.Find("Pauser").GetComponent<Pauser>().The_end();
        Destroy(gameObject);
    }
}
