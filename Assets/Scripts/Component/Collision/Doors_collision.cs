using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Doors_collision : MonoBehaviour // Перемещает игрока меж комнатами
{
    public bool open = true;
    public AudioSource aud_open;
    public AudioSource aud_unlock;
    public AudioSource aud_close;
    public AudioSource aud_pass_through;
    public SpriteRenderer sprite_rend;
    private Sprite[] sprites;
    private bool locked = false;
    public Text amount;
    void Awake() {
        sprites = Resources.LoadAll<Sprite>("Doors");
    }
    private void Start() {
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
        aud_pass_through.Play();
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
            aud_close.Play();
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
        aud_unlock.Play();
    }
}

