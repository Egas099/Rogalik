﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill_collision : MonoBehaviour
{
    public Sounds_manager s_m;
    private void Start() {
        s_m = GameObject.Find("SoundsManager").GetComponent<Sounds_manager>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            switch (gameObject.name)
            {
                case "attack_speed_up":
                other.gameObject.GetComponent<Entity>().Stat_changed("damage",-0.5f);
                other.gameObject.GetComponent<Entity>().Stat_changed("attack_speed",1.5f);
                break;
                case "damage_up": 
                other.gameObject.GetComponent<Entity>().Stat_changed("damage",1f);
                break;
                case "speed_up": 
                other.gameObject.GetComponent<Entity>().Stat_changed("speed",0.5f);
                break;
                default:break;
            }
            s_m.bite.Play();
            Destroy(gameObject);
        }
    }
}
