using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public Sounds_manager s_m;
    private GameObject ban;
    public GameObject main_room;
    private GameObject[] bonus_room;
    private GameObject[] monster_room;
    private GameObject[] trap_room;
    private GameObject[] boss_room;
    private GameObject[,] map;
    private GameObject create_room;
    private GameObject[] consumables;

    public GameObject[] character;
    private GameObject use_character;
    public Random rnd = new Random();

    private int xlen = 9, ylen = 9;
    public int room_limit_bonus = 3, room_limit_enemy = 10, room_limit_trap = 2;
    private int room_limit_all;
    
    public void Select_char(string name){
        foreach (GameObject ch in character)
        {
            if (ch.name == name)
                use_character = ch;
        }
        use_character = Instantiate(use_character, new Vector3(0,0,-0.5f), Quaternion.identity);
        gameObject.GetComponent<Checking_character_position>().Start();
    } 
    public void ClearAll(){
        Clearing();
        Destroy(use_character);
    }
    bool Rand()//absolutely random function
    {
        return (Mathf.RoundToInt(Random.Range(0f, 1f))==1)?true:false;
    }
    void Awake()
    {
        map = new  GameObject[xlen, ylen];
        Load_Rooms();
    }
    void Load_Rooms() {
        bonus_room =  Resources.LoadAll<GameObject>("BonusRooms");
        monster_room =  Resources.LoadAll<GameObject>("MonsterRooms");
        trap_room = Resources.LoadAll<GameObject>("TrapRooms");
        boss_room = Resources.LoadAll<GameObject>("BossRooms");
    }
   
    
    void FixedUpdate() {
        if (Input.GetKey(KeyCode.Q))
            New_level();
    }
    public void New_level(){
        Clearing();
        Generation();
        gameObject.GetComponent<Checking_character_position>().Change_pos();
        s_m.level_up.Play();
        gameObject.GetComponent<Score_manager>().level++;
    }
    public void Generation(){
        use_character.transform.position = new Vector3(0,0,0);
        ban = new GameObject("empty");
        room_limit_all = room_limit_bonus + room_limit_enemy + room_limit_trap + 1;
        int room_limit_bonus_be = 0, room_limit_enemy_be = 0, room_limit_trap_be = 0, room_limit_all_be = 0;
        map[3, 3] = main_room; map[2, 2] = ban;map[4, 4] = ban;map[2, 4] = ban;map[4, 2] = ban;

        while (room_limit_all_be < room_limit_all)
        {
            for (int i = 2; i < xlen - 2; i++){
                for (int j = 2; j < ylen - 2; j++)
                {
                    if ((map[i, j] != null) && (map[i, j] != ban) && (map[i, j].GetComponent<Room_info>().room_name != "Bonus_room"))
                    {
                        for (int u = -1; u <= 1; u++){
                            for (int v = -1; v <= 1; v++)
                            {
                                if ((map[i + u, j + v] == null) && (room_limit_all_be < room_limit_all) && (Mathf.Abs(v) != Mathf.Abs(u)) && Rand())
                                {
                                    
                                    if (room_limit_all_be == room_limit_all - 1 - room_limit_bonus)
                                    {
                                        map[i + u, j + v] = boss_room[Random.Range(0, boss_room.Length)];
                                        room_limit_all_be++;
                                        break;
                                    }
                                    if (room_limit_all_be >= room_limit_all - room_limit_bonus)
                                    {
                                        map[i + u, j + v] = bonus_room[Random.Range(0, bonus_room.Length)];
                                        room_limit_all_be++;
                                        room_limit_bonus_be++;
                                    }
                                    else
                                    {
                                        switch (Mathf.RoundToInt(Random.Range(3f, 5f)))
                                        {
                                            case 3:
                                                if (room_limit_enemy_be < room_limit_enemy)
                                                {
                                                    map[i + u, j + v] = monster_room[Random.Range(0, monster_room.Length)];
                                                    room_limit_all_be++;
                                                    room_limit_enemy_be++;
                                                }
                                                break;
                                            case 4:
                                                if (room_limit_trap_be < room_limit_trap)
                                                {
                                                    map[i + u, j + v] = trap_room[Random.Range(0, trap_room.Length)];
                                                    room_limit_all_be++;
                                                    room_limit_trap_be++;
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        for (int i = 0; i < xlen; i++){
            for (int j = 0; j < ylen; j++)
            {
                if((map[i, j] != null) && (map[i, j] != ban))
                {
                    create_room = Instantiate(map[i, j], new Vector3(i*8-24,j*5-15,1), Quaternion.identity);
                    if((map[i+1, j] == null) || (map[i+1, j] == ban)){
                        Destroy(create_room.transform.Find("right_door").gameObject);
                    }
                    else
                        if (map[i+1, j].GetComponent<Room_info>().room_name == "Bonus_room")
                            create_room.GetComponent<Room_info>().Lock_door("right_door");
                    if((map[i-1, j] == null) || (map[i-1, j] == ban)){
                        Destroy(create_room.transform.Find("left_door").gameObject);
                    }
                    else
                        if (map[i-1, j].GetComponent<Room_info>().room_name == "Bonus_room")
                            create_room.GetComponent<Room_info>().Lock_door("left_door");
                    if((map[i, j+1] == null) || (map[i, j+1] == ban)){
                        Destroy(create_room.transform.Find("up_door").gameObject);
                    }
                    else
                        if (map[i, j+1].GetComponent<Room_info>().room_name == "Bonus_room")
                            create_room.GetComponent<Room_info>().Lock_door("up_door");
                    if((map[i, j-1] == null) || (map[i, j-1] == ban)){
                        Destroy(create_room.transform.Find("down_door").gameObject);
                    }
                    else
                        if (map[i, j-1].GetComponent<Room_info>().room_name == "Bonus_room")
                            create_room.GetComponent<Room_info>().Lock_door("down_door");
                    map[i, j] = create_room;
                }
            }
        }
        GameObject.Find("Text_key").GetComponent<Text>().text = "X" + use_character.GetComponent<Entity>().number_keys;
        gameObject.GetComponent<Checking_character_position>().Start();   
    }
    void Clearing()
    {
        for (int i = 0; i < xlen; i++){
            for (int j = 0; j < ylen; j++)
            {
                Destroy(map[i,j]);
                map[i,j] = null;
            }   
        }
        consumables = GameObject.FindGameObjectsWithTag("Consumables");
        for (int i = consumables.Length-1; i >= 0; i--){
            Destroy(consumables[i]);
        }
        consumables = GameObject.FindGameObjectsWithTag("Monster");
        for (int i = consumables.Length-1; i >= 0; i--){
            Destroy(consumables[i]);
        }
    }
}
    

