using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_collision : MonoBehaviour
{
    public float damage;
    public float attack_speed;
    private float last_fire;
    private void Start() {
        attack_speed = 6/attack_speed;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if ((last_fire + attack_speed)  < Time.time)    // Если прошлло больше времени, чем скорость атаки
        {
            switch (other.gameObject.tag){  // Приверяем с чем столкнулись
                case "Player":
                other.gameObject.GetComponent<Entity>().Stat_changed("health",-damage);
                last_fire = Time.time;
                break;
                default:break;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if ((last_fire + attack_speed)  < Time.time)    // Если прошлло больше времени, чем скорость атаки
        {
            switch (other.gameObject.tag){  // Приверяем с чем столкнулись
                case "Player":
                other.gameObject.GetComponent<Entity>().Stat_changed("health",-damage);
                last_fire = Time.time;
                break;
                default:break;
            }
        }
    }
}
