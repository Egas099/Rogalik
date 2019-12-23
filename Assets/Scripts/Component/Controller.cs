using UnityEngine;
using UnityEngine.UI;
public class Controller : MonoBehaviour
{
    public GameObject chits;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Entity entity;
    private float step_time;
    private float last_step_time;
    private float x, y;
    private KeyCode[] control;
    private but[] _input;
    // Start is called before the first frame update
    void Start()
    {
        control = new KeyCode[8];
        Key_assignment();
        entity = GetComponent<Entity>();
        rb = GetComponent<Rigidbody2D>();
        step_time = 1.3f / entity.speed;
        last_step_time = Time.time;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Attack();
    }
    void Movement()
    {
        direction = new Vector2(0, 0);
        if (Input.GetKey(control[0]))
            direction += new Vector2(0, 1f * entity.speed);
        if (Input.GetKey(control[2]))
            direction += new Vector2(0, -1f * entity.speed);
        if (Input.GetKey(control[4]))
            direction += new Vector2(-1f * entity.speed, 0);
        if (Input.GetKey(control[6]))
            direction += new Vector2(1f * entity.speed, 0);
        if (direction != Vector2.zero)
        {
            // if ((last_step_time + step_time) < Time.time)
            //     last_step_time = Time.time;
            rb.AddForce(direction);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                GameObject.Find("logic").GetComponent<Score_manager>().Add_score(100000);
                GameObject.Find("UI_chets").gameObject.GetComponent<Text>().text = "Использованы чит коды";
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                entity.Stat_changed("health",1);
                GameObject.Find("UI_chets").gameObject.GetComponent<Text>().text = "Использованы чит коды";
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                GameObject.Find("logic").GetComponent<Score_manager>().Add_key();
                GameObject.Find("UI_chets").gameObject.GetComponent<Text>().text = "Использованы чит коды";
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                entity.Stat_changed("damage",1);
                GameObject.Find("UI_chets").gameObject.GetComponent<Text>().text = "Использованы чит коды";
            }
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                entity.Stat_changed("speed",1);
                GameObject.Find("UI_chets").gameObject.GetComponent<Text>().text = "Использованы чит коды";
            }
            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                entity.Stat_changed("attack_speed",1);
                GameObject.Find("UI_chets").gameObject.GetComponent<Text>().text = "Использованы чит коды";
            }
            if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                if (entity.attack_type == "triple")
                    entity.attack_type = "single";
                else
                    entity.attack_type = "triple";
                GameObject.Find("UI_chets").gameObject.GetComponent<Text>().text = "Использованы чит коды";
            }
        }
    }
    void Attack()
    {
        if (Input.GetKey(control[1]))
            entity.attack_system.Fire(transform.position, new Vector2(0, 2f),entity.attack_type);//вверх
        if (Input.GetKey(control[3]))
            entity.attack_system.Fire(transform.position + new Vector3(0, -0.3f, 0), new Vector2(0, -2f),entity.attack_type);//вниз
        if (Input.GetKey(control[5]))
            entity.attack_system.Fire(transform.position + new Vector3(-0.1f,0 , 0), new Vector2(-2f, 0),entity.attack_type);//влево
        if (Input.GetKey(control[7]))
            entity.attack_system.Fire(transform.position + new Vector3(+0.1f,0, 0), new Vector2(2f, 0),entity.attack_type);//вправо
    }
    void Key_assignment()
    {
        _input = GameObject.Find("Setting").gameObject.GetComponent<inputGame>()._input;
        for (int i = 0; i < 8; i++)
        {
            control[i] = _input[i].keyCode;
        }
    }
}

