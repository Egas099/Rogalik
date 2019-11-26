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
    public float speed;
    [HideInInspector]
    public int number_keys = 0;
    [HideInInspector]
    public Attack_system attack_system;

    public AudioSource greeting;        // Приветствие
    public AudioSource aud_move;        // Перемещение
    public AudioSource health_down;     // Получение урона
    public AudioSource health_up;       // Выздоровление
    public AudioSource destroy_shell;   // Уничтожение снаряда
    public AudioSource range_attack;    // Выстрел
    public AudioSource melee_attack;    // Удар
    public AudioSource low_health;
    public AudioClip aud_die;

    public GameObject die_obj;
    private UI_hearts hearts_UI;
    private Sounds_manager s_m;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        hearts_UI = GameObject.Find("UI_heart").gameObject.GetComponent<UI_hearts>();
        s_m = GameObject.Find("SoundsManager").GetComponent<Sounds_manager>();
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
        {
            Die();
        }
        if (low_health != null)
        {
            if ((health/max_health)<0.30)
                low_health.mute = false;
            else
                low_health.mute = true;
        }
        
    }
    public void Stat_changed(string what, float how_much){ // Метод изменяющий характеристики сущности
        switch (what)
        {
            case "health":
            if(how_much > 0)
            {
                if(health_up != null)
                    health_up.Play();
            }
            else
            {
                if(health_down != null)
                    health_down.Play();
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
            if (greeting!=null)
                greeting.Play();
        }
    }
    void Die(){
        enable = false;
        if (gameObject.tag == "Monster")
            GameObject.Find("logic").GetComponent<Score_manager>().Add_score(30);
        if(aud_die!=null)
            s_m.Play_pls(aud_die);
        if(gameObject.GetComponent<Drop>())
            gameObject.GetComponent<Drop>().Drop_(transform.position);
        if(die_obj)
            Instantiate(die_obj,transform.position,Quaternion.identity);
        if (gameObject.tag == "Player")
                GameObject.Find("Pauser").GetComponent<Pauser>().The_end();
        Destroy(gameObject);
    }
}
