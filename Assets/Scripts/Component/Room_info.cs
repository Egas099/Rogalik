using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_info : MonoBehaviour
{
    public string room_name;
    public bool cleared = false; // Обозначает пройдена ли комната(все ли монстры убиты и т.д.)
    private GameObject clild_obj; // Потомок(Монстр)
    private GameObject door;
    public Random rnd = new Random();
    private Sounds_manager s_m;
    
    private void Start() {
        s_m = GameObject.Find("SoundsManager").GetComponent<Sounds_manager>();
        if (room_name == "Main_room")
            cleared = true;
    }

    public void Check_cleared(){
        foreach(Transform child in transform)
        {
            if(child.tag == "Monster")
                return;
            if(child.tag == "Trap_button")
            {
                if(child.GetComponent<Button_collision>().click == false)
                    return;
            }
            if (child.tag == "Consumables")
                return;
        }
        if(room_name == "Boss_room"){
            Drop();
            s_m.Boss_track(false);
            }

        if(room_name == "Monster_room")
            Drop();
        Open_door();
        if(room_name == "Bonus_room")
            GameObject.Find("logic").GetComponent<Generator>().Doors_recalc();
        
    }
    void Drop()
    {
        if(gameObject.GetComponent<Drop>())
            gameObject.GetComponent<Drop>().Drop_(transform.position);
    }
    void OnTransformChildrenChanged()
    {
        Check_cleared();
    }
    public void Close_door(){
        foreach(Transform child in transform)
        {
            if(child.tag == "Door"){
                child.GetComponent<Doors_collision>().Close();
            }
        }
    }
    public void Open_door(){
        if (!cleared)
            GameObject.Find("logic").GetComponent<Score_manager>().Add_score(30);
        cleared = true;
        foreach(Transform child in transform)
        {
            if(child.tag == "Door"){
                child.GetComponent<Doors_collision>().Open();
            }
        }
    }
    public void Enable_monsters()
    {
        foreach(Transform child in transform)
            {
                if(child.tag == "Monster")
                {
                    child.transform.position = new Vector3(transform.position.x-Random.Range(-2f, 2f),transform.position.y-Random.Range(-1f, 1f),0.1f);
                    child.GetComponent<Entity>().Enable_entity();
                }
            }
    }
    public void Lock_door(string door_name){
        foreach(Transform child in transform)
        {
            if((child.tag == "Door") && (child.name == door_name)){
                child.GetComponent<Doors_collision>().Lock();
            }
        }
    }
    public void Unlock_door(string door_name){
        foreach(Transform child in transform)
        {
            if((child.tag == "Door") && (child.name == door_name)){
                child.GetComponent<Doors_collision>().Un_lock();
            }
        }
    }
    
}
