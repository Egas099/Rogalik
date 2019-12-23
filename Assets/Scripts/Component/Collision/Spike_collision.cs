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
            if(other.gameObject.tag == "Player"){  // Приверяем с чем столкнулись
                other.gameObject.GetComponent<Entity>().Stat_changed("health",-damage);
                last_fire = Time.time;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (last_fire + attack_speed < Time.time)    // Если прошлло больше времени, чем скорость атаки
        {
            if (other.gameObject.tag == "Player"){  // Приверяем с чем столкнулись
                last_fire = Time.time;
                other.gameObject.GetComponent<Entity>().Stat_changed("health",-damage);
            }
        }
    }
}
