using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart_collision : MonoBehaviour
{
    private Entity entity;
    private void OnCollisionEnter2D(Collision2D other) {
        if ((other.gameObject.tag == "Player")&&(other.gameObject.GetComponent<Entity>().health < other.gameObject.GetComponent<Entity>().max_health))
        {
            other.gameObject.GetComponent<Entity>().Stat_changed("health",1);
            Destroy(gameObject);
        }
    }
}
