using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key_collision : MonoBehaviour
{
    public AudioClip clip;
    public Text amount;
    private void Start() {
        amount = GameObject.Find("Text_key").gameObject.GetComponent<Text>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Entity>().number_keys++;
            amount.text = "x" + other.gameObject.GetComponent<Entity>().number_keys;
            GameObject.Find("SoundsManager").GetComponent<Sounds_manager>().Play_request(clip);
            Destroy(gameObject);
            
        }
    }
}
