using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private Vector2 direction;
    private Entity entity;
    private float step_time;
    private float last_step_time;
    private float x,y;
    private KeyCode[] control;
    private but[] _input;
    // Start is called before the first frame update
    void Start()
    {
        control = new KeyCode[8];
        Key_assignment();
        entity = GetComponent<Entity> ();
        rb = GetComponent<Rigidbody2D> ();
        step_time = 1.3f/entity.speed;
        last_step_time = Time.time;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Attack();
    }
    void Movement(){
        direction = new Vector2(0,0);
        if(Input.GetKey(control[0]))
            direction += new Vector2(0,1f*entity.speed);
        if(Input.GetKey(control[2]))
            direction += new Vector2(0,-1f*entity.speed);
        if(Input.GetKey(control[4]))
            direction += new Vector2(-1f*entity.speed,0);
        if(Input.GetKey(control[6]))
            direction += new Vector2(1f*entity.speed,0);
        if (direction != Vector2.zero)
        {   
            // if ((last_step_time + step_time) < Time.time)
            //     last_step_time = Time.time;
            rb.AddForce(direction);
        }
    }
    void Attack()
    {
        if (Input.GetKey(control[1]))
            entity.attack_system.Fire(transform.position, new Vector2(0,2f));
        if (Input.GetKey(control[3]))
            entity.attack_system.Fire(transform.position + new Vector3(0,-0.1f,0), new Vector2(0,-2f));
        if (Input.GetKey(control[5]))
            entity.attack_system.Fire(transform.position, new Vector2(-2f,0));
        if (Input.GetKey(control[7]))
            entity.attack_system.Fire(transform.position, new Vector2(2f,0));
    }
    void Key_assignment(){
        _input = GameObject.Find("Setting").gameObject.GetComponent<inputGame>()._input;
        for (int i = 0; i < 8; i++)
        {
            control[i] = _input[i].keyCode;
        }
    }
}

