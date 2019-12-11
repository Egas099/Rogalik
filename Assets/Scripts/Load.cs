using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public GameObject loadline;
    public GameObject Menu;
    public Highscores highscores;
    public Main volume;
    void Update()
    {
        loadline.transform.position = new Vector3(loadline.transform.position.x + 0.1f, loadline.transform.position.y, loadline.transform.position.z);
        if (loadline.transform.position.x >= 0)
        {
            Menu.SetActive(true);
            Destroy(gameObject);
        }
        if (loadline.transform.position.x >= 50)
        {
            highscores.Start();
            volume.Start();
        }
    }
}
