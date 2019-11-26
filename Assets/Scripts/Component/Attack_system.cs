using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_system : MonoBehaviour { // Система атаки. Основной метод Fire() вызывается, когда юнит "хочет" выстрелить

    // Start is called before the first frame update
    public bool melee_attack;   // Атакует ли в ближнем бою
    // public string attack_type;  // Тип атаки
    public GameObject shell;

    private GameObject created_shell;
    private Entity entity;
    private float last_fire_range;      // момент последней дальней атаки
    private float last_fire_melle;     // момент последней ближней атаки
    private Rigidbody2D rb_shell;
    // Update is called once per frame
    
    void Start() {
        entity = GetComponent<Entity> ();
        last_fire_range = Time.time;
        last_fire_melle = Time.time;
    }
    public void Fire(Vector3 position,Vector2 direction){    // Выстреливает из position в сторону direction
        if ((last_fire_range + (6/entity.attack_speed))  < Time.time)
        {
            last_fire_range = Time.time;
            if(entity.range_attack!=null)
                entity.range_attack.Play();
            created_shell = Instantiate(shell,position,Quaternion.identity);
            created_shell.GetComponent<Shell_collision>().damage = entity.damage;
            created_shell.GetComponent<Shell_collision>().owner = gameObject.tag;
            created_shell.GetComponent<Rigidbody2D>().AddForce(direction);
        }   
    }
    public void Fire(Vector3 position){ // Создаёт объект в position 
        if ((last_fire_range + (6/entity.attack_speed)) < Time.time)
            {
                last_fire_range = Time.time;
                if(entity.range_attack!=null)
                    entity.range_attack.Play();
                created_shell = Instantiate(shell,position,Quaternion.identity);
                if (created_shell.GetComponent<Entity>() != null)
                    created_shell.GetComponent<Entity>().enable = true;
            }
    }
    public void Fire(Vector3 position,Vector2 direction, string type){    // Выстреливает из position в сторону direction
        switch(type)
        {
            case "triple":
                if ((last_fire_range + (6/entity.attack_speed))  < Time.time)
            {
                last_fire_range = Time.time;
                if(entity.range_attack!=null)
                    entity.range_attack.Play();
                created_shell = Instantiate(shell,position,Quaternion.identity);
                created_shell.GetComponent<Shell_collision>().damage = entity.damage;
                created_shell.GetComponent<Shell_collision>().owner = gameObject.tag;
                created_shell.GetComponent<Rigidbody2D>().AddForce(direction);
                created_shell = Instantiate(shell,position,Quaternion.identity);
                created_shell.GetComponent<Shell_collision>().damage = entity.damage;
                created_shell.GetComponent<Shell_collision>().owner = gameObject.tag;
                created_shell.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x+(direction.y*0.5f),direction.y-(direction.x*0.5f)));
                created_shell = Instantiate(shell,position,Quaternion.identity);
                created_shell.GetComponent<Shell_collision>().damage = entity.damage;
                created_shell.GetComponent<Shell_collision>().owner = gameObject.tag;
                created_shell.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x-(direction.y*0.5f),direction.y+(direction.x*0.5f)));
            }
            break;
            default: Debug.Log("Bad type of attack");break;
        }
    }
    void OnCollisionStay2D(Collision2D other) { // Вызывается каждый кадр при столкновении с чем-нибудь

        if ((entity.enable) && (melee_attack))
        {
            if ((last_fire_melle + (6/entity.attack_speed))  < Time.time)    // Если с момента последней атаки прошлло больше времени, чем скорость атаки
            {
                switch (other.gameObject.tag){  // Приверяем с чем столкнулись
                    case "Player":
                    other.gameObject.GetComponent<Entity>().Stat_changed("health",-entity.damage);
                    last_fire_melle = Time.time;
                    break;
                    default:break;
                }
            }
        }
    }
}
