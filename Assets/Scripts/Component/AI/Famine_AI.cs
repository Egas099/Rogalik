using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Famine_AI : MonoBehaviour
{
    private Entity entity;
    private Transform target;
    private Vector2 direction;
    private Rigidbody2D rb;

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponent<Entity>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        speed = entity.speed;
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {   
        if(entity.enable == true)
        {
            direction = new Vector2(target.position.x-transform.position.x,target.position.y-transform.position.y);
            while ((Mathf.Abs(direction.x)>2) || (Mathf.Abs(direction.y)>2))
            {
                direction.x /= 2;
                direction.y /= 2;
            }
            direction = new Vector2((direction.x*speed),(direction.y*speed));
            rb.AddForce(direction);
        }
    }
    void OnCollisionEnter2D(Collision2D other) {
        if((entity.enable == true) && ((other.gameObject.tag == "Room")||(other.gameObject.tag == "Door")))
        {
            direction = new Vector2(0,0);
            direction.x = target.position.x-transform.position.x;
            direction.y = target.position.y-transform.position.y;
            while ((Mathf.Abs(direction.x)>2) || (Mathf.Abs(direction.y)>2))
            {
                direction.x /= 2;
                direction.y /= 2;
            }
            entity.attack_system.Fire(transform.position, direction,"triple");
            
        }
    }
}
