using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gut_AI : MonoBehaviour
{
    private Entity entity;
    private Rigidbody2D rb;
    private Vector2 target;
    private float speed;
    private float last_jump;
    // Start is called before the first frame update
    void Start()
    {
        last_jump = Time.time;
        entity = GetComponent<Entity>();
        speed = entity.speed;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(entity.enable == true)
        {
            if ((last_jump + (6/entity.speed)) < Time.time)
            {
                rb.AddForce(new Vector2(Random.Range(-4f, 4f),Random.Range(-4f, 4f)));
                last_jump = Time.time;
                entity.attack_system.Fire(new Vector3(transform.position.x,transform.position.y,transform.position.z+0.1f));
                entity.aud_move.Play();
            }
        }
    }
}
