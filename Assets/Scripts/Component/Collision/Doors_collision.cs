using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Doors_collision : MonoBehaviour // Перемещает игрока меж комнатами
{
    public bool open = true;
    public AudioClip aud_open;
    public AudioClip aud_unlock;
    public AudioClip aud_close;
    public AudioClip aud_pass_through;
    public SpriteRenderer sprite_rend;
    private Sprite[] sprites;
    private bool locked = false;
    [HideInInspector]
    public Text amount;
    [HideInInspector]
    public Sounds_manager sounds_Manager;

    void Awake() {
        sprites = Resources.LoadAll<Sprite>("Doors");
    }
    private void Start() {
        sounds_Manager = GameObject.Find("SoundsManager").GetComponent<Sounds_manager>();
        amount = GameObject.Find("Text_key").gameObject.GetComponent<Text>();
    }
    void Skip_charcter(Vector3 pos, Transform tr){
        switch(name){
            case "up_door":
            tr.position = new Vector3(pos.x,pos.y+1.8f,pos.z);
            break;
            case "down_door":
            tr.position = new Vector3(pos.x,pos.y-1.8f,pos.z);
            break;
            case "left_door":
            tr.position = new Vector3(pos.x-1.7f,pos.y,pos.z);
            break;
            case "right_door":
            tr.position = new Vector3(pos.x+1.7f,pos.y,pos.z);
            break;
        }
        sounds_Manager.Play_request(aud_pass_through);
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            if(open == true){
                Skip_charcter(other.transform.position, other.transform);
                GameObject.Find("logic").GetComponent<Checking_character_position>().Change_pos();
                return;
            }
            if (locked && (other.gameObject.GetComponent<Entity>().number_keys > 0) && transform.parent.gameObject.GetComponent<Room_info>().cleared)
            {
                other.gameObject.GetComponent<Entity>().number_keys--;
                amount.text = "x" + other.gameObject.GetComponent<Entity>().number_keys;
                Un_lock();
                Skip_charcter(other.transform.position, other.transform);
                GameObject.Find("logic").GetComponent<Checking_character_position>().Change_pos();
                return;
            }
        }
    }

    public void Close()
    {
        if (!locked)
        {
            if (aud_close != null)
            sounds_Manager.Play_request(aud_close);
        sprite_rend.sprite = sprites[0];
        open = false;
        }
    }
    public void Open()
    {
        if (!locked)
        {
            sprite_rend.sprite = sprites[2];
            open = true;
        }
    }
    public void Lock(){
        sprite_rend.sprite = sprites[1];
        open = false;
        locked = true;
    }
    public void Un_lock(){
        locked = false;
        open = true;
        sprite_rend.sprite = sprites[2];
        sounds_Manager.Play_request(aud_unlock);
    }
}

