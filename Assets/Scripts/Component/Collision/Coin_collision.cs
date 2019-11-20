﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_collision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("logic").GetComponent<Score_manager>().Add_score(100);
            GameObject.Find("SoundsManager").GetComponent<Sounds_manager>().take_coin.Play();
            Destroy(gameObject);
        }
    }
}
