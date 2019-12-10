using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checking_character_position : MonoBehaviour //  Расчитывает в какой комнате находится персонаж, закрывает или открывает двери, а также перемещает камеру
{
    private Transform character;    // Компонент Transform персонажа
    private GameObject curent_room; // Текущая комната
    private GameObject[] all_room;  // Все комнаты
    private Vector3 curent_ch_position; // Текущая позиция персонажа
    private Room_info curent_Room_info;  // Скрипт комнаты
    public Sounds_manager s_m;
    // Start is called before the first frame update
    public void Start()
    {
        character = GameObject.FindWithTag("Player").transform;
        Calc_pos();
    }
    void Calc_pos() // Расчитывает центр в области(там находится комната), где находится персонаж
    {
        curent_ch_position = character.position;
        curent_ch_position.x = Mathf.Round(curent_ch_position.x/10f)*10f;
        curent_ch_position.y = Mathf.Round(curent_ch_position.y/6f)*6f;
        curent_ch_position = new Vector3(Mathf.Round(curent_ch_position.x),Mathf.Round(curent_ch_position.y),-2f);

        GameObject.Find("Main Camera").transform.position = curent_ch_position; // Попутно перемещаем камеру
    }
    public void Change_pos()    // Вызов метода происходит когда персонаж переходит в другую комнату
    {
        Calc_pos();
        all_room = GameObject.FindGameObjectsWithTag("Room");// Берем все комнаты
        foreach (var room in all_room)  // Ищем нужную
        {
            if((room.transform.position.y == curent_ch_position.y)&&(room.transform.position.x == curent_ch_position.x))
            {
                curent_room = room;
                break;
            }
        }
        curent_Room_info = curent_room.GetComponent<Room_info> ();
        if(!curent_Room_info.cleared)
            {
                curent_Room_info.Close_door();
                if((curent_Room_info.room_name == "Monster_room") || ((curent_Room_info.room_name == "Boss_room")))
                {
                    curent_Room_info.Enable_monsters();
                }
                if(curent_Room_info.room_name == "Boss_room")
                    s_m.Boss_track(true);
            }
    }
    // Update is called once per frame
}
