using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell_collision : MonoBehaviour
{   
    public AudioClip destroy_shell;   
    public float time_of_living;
    [HideInInspector]
    public string owner;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public Sounds_manager sounds_Manager;

    private void Start() {
        time_of_living = Time.time + time_of_living;
        sounds_Manager = GameObject.Find("SoundsManager").GetComponent<Sounds_manager>();

    }
    private void FixedUpdate() {
        if(time_of_living < Time.time)
            Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(owner == "Player"){
            if (other.gameObject.tag == "Monster")
            {
                other.gameObject.GetComponent<Entity>().Stat_changed("health",-damage);
                if(!destroy_shell)
                    sounds_Manager.Play_request(destroy_shell);
                Destroy(gameObject);
            }
        }
        if (owner == "Monster")
        {
             if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Entity>().Stat_changed("health",-damage);
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Room"){
            sounds_Manager.Play_request(destroy_shell);
            Destroy(gameObject);
        }
    }
}
