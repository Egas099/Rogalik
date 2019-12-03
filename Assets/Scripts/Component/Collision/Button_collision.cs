using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_collision : MonoBehaviour
{
    public bool click = false;
    public AudioClip aud_click;
    public SpriteRenderer sprite_rend;
    private Sprite[] sprites;
    public Sounds_manager sounds_Manager;

    void Awake() {
        sounds_Manager = GameObject.Find("SoundsManager").GetComponent<Sounds_manager>();
        sprites = Resources.LoadAll<Sprite>("Button");
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if((click == false)&&(other.gameObject.tag == "Player")){
            sprite_rend.sprite = sprites[1];
            click = true;
            gameObject.transform.parent.gameObject.GetComponent<Room_info>().Check_cleared();
            sounds_Manager.Play_request(aud_click);
        }
    }
}
