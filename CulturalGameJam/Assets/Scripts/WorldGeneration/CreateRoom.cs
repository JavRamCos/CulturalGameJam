using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoom : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] rooms;
    public GameObject[] playerSpawn;

    private int direccion;
    public float moveAmount = 10f;

    private float time;
    public float startTime = 0.25f;

    public float minX;
    public float maxX;
    public float minY;
    public bool stop;

    public GameObject[] world_items;

    public LayerMask room;

    private int dCount;


    bool spwn = false;
    public int plumaCount = 0;
    public int countLim = 2;

    public GameObject next_Area;
    public GameObject current_Area;
    public GameObject prev_Area;

    Teleporter teleport;


    // Start is called before the first frame update
    void Start()
    {
        int rndSpawnPoint = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[rndSpawnPoint].position;
        
        var room = Instantiate(rooms[0], transform.position, Quaternion.identity);
        room.transform.parent = current_Area.transform;
        if (playerSpawn[0].tag != "Player")
        {
            var newTele = Instantiate(playerSpawn[0], new Vector2(transform.position.x, transform.position.y + 5), Quaternion.identity);
            newTele.transform.parent = current_Area.transform;

            teleport = newTele.GetComponent<Teleporter>();
            teleport.destination = prev_Area.transform.GetChild(0).transform;

        }
        else
        {
            //var tp =
            Instantiate(playerSpawn[0], new Vector2(transform.position.x, transform.position.y + 5), Quaternion.identity);
            //tp.transform.parent = current_Area.transform;
        }
        

        direccion = Random.Range(1, 6);

    }

    // Update is called once per frame
    void Update()
    {

        if (time <= 0 && stop == false)
        {

            Move();
            time = startTime;
        }
        else
        {
            time -= Time.deltaTime;
        }

    }

    private void Move()
    {

        if (direccion == 1 || direccion == 2)
        {
            if (transform.position.x < maxX)//Derecha
            {
                dCount = 0;

                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                var room = Instantiate(rooms[rand], transform.position, Quaternion.identity);
                room.transform.parent = current_Area.transform;

                direccion = Random.Range(1, 6);
                if (direccion == 3)
                {
                    direccion = 2;
                }
                if (direccion == 4)
                {
                    direccion = 5;
                }

                int rand_item = Random.Range(0, world_items.Length);
                if(rand_item == 0 && spwn == false)
                {
                    var item = Instantiate(world_items[rand_item], new Vector2(transform.position.x, transform.position.y + 4), Quaternion.identity);
                    item.transform.parent = current_Area.transform;
                    spwn = true;
                }
                else if(rand_item == 1 && plumaCount < countLim)
                {
                    var item = Instantiate(world_items[rand_item], new Vector2(transform.position.x, transform.position.y + 4), Quaternion.identity);
                    item.transform.parent = current_Area.transform;
                    plumaCount += 1;
                    //Debug.Log("PLUMA");
                }
                else if (rand_item == 2)
                {
                    var item = Instantiate(world_items[rand_item], new Vector2(transform.position.x, transform.position.y + 4), Quaternion.identity);
                    item.transform.parent = current_Area.transform;
                    spwn = true;
                }
            }
            else
            {
                direccion = 5;
            }

        }
        else if (direccion == 3 || direccion == 4)//Izquierda
        {
            if (transform.position.x > minX)
            {
                dCount = 0;

                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                var room = Instantiate(rooms[rand], transform.position, Quaternion.identity);
                room.transform.parent = current_Area.transform;

                direccion = Random.Range(3, 6);

                int rand_item = Random.Range(0, 2);
                if (rand_item == 0 && spwn == false)
                {
                    var item = Instantiate(world_items[rand_item], new Vector2(transform.position.x, transform.position.y + 4), Quaternion.identity);
                    item.transform.parent = current_Area.transform;
                    spwn = true;
                }
                else if (rand_item == 1 && plumaCount < countLim)
                {
                    var item = Instantiate(world_items[rand_item], new Vector2(transform.position.x, transform.position.y + 4), Quaternion.identity);
                    item.transform.parent = current_Area.transform;
                    plumaCount += 1;
                    //Debug.Log("PLUMA");
                }
            }
            else
            {
                direccion = 5;
            }


        }
        if (direccion == 5)//Abajo
        {
            dCount += 1;

            if (transform.position.y > minY)
            {

                Collider[] roomDetection = Physics.OverlapSphere(transform.position, 1, room, QueryTriggerInteraction.Ignore);


                if (roomDetection[0].GetComponent<RoomType>().type != 1 && roomDetection[0].GetComponent<RoomType>().type != 3)
                {
                    if (dCount >= 2)
                    {
                        roomDetection[0].GetComponent<RoomType>().DestroyRoom();
                        var room = Instantiate(rooms[3], transform.position, Quaternion.identity);
                        room.transform.parent = current_Area.transform;

                        int rand_item = Random.Range(0, 2);
                        if (rand_item == 0 && spwn == false)
                        {
                            var item = Instantiate(world_items[rand_item], new Vector2(transform.position.x, transform.position.y + 4), Quaternion.identity);
                            item.transform.parent = current_Area.transform;
                            spwn = true;
                        }
                        else if (rand_item == 1 && plumaCount < countLim)
                        {
                            var item = Instantiate(world_items[rand_item], new Vector2(transform.position.x, transform.position.y + 4), Quaternion.identity);
                            item.transform.parent = current_Area.transform;
                            plumaCount += 1;
                            //Debug.Log("PLUMA");
                        }
                    }
                    else
                    {
                        roomDetection[0].GetComponent<RoomType>().DestroyRoom();

                        int randomBottom = Random.Range(1, rooms.Length);
                        if (randomBottom == 2)
                        {
                            randomBottom = 1;
                        }
                        var room = Instantiate(rooms[randomBottom], transform.position, Quaternion.identity);
                        room.transform.parent = current_Area.transform;

                        int rand_item = Random.Range(0, 2);
                        if (rand_item == 0 && spwn == false)
                        {
                            var item = Instantiate(world_items[rand_item], new Vector2(transform.position.x, transform.position.y + 4), Quaternion.identity);
                            item.transform.parent = current_Area.transform;
                            spwn = true;
                        }
                        else if (rand_item == 1 && plumaCount < countLim)
                        {
                            var item = Instantiate(world_items[rand_item], new Vector2(transform.position.x, transform.position.y + 4), Quaternion.identity);
                            item.transform.parent = current_Area.transform;
                            plumaCount += 1;
                            //Debug.Log("PLUMA");
                        }
                    }


                }


                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                var rand_room = Instantiate(rooms[rand], transform.position, Quaternion.identity);
                rand_room.transform.parent = current_Area.transform;

                direccion = Random.Range(1, 5);
            }
            else
            {
                var newTele = Instantiate(playerSpawn[1], new Vector2(transform.position.x, transform.position.y + 2.5f), Quaternion.identity);
                newTele.transform.parent = gameObject.transform;

                teleport = newTele.GetComponent<Teleporter>();

                teleport.destination = next_Area.transform.GetChild(next_Area.transform.childCount - 1).transform;


                if (gameObject.transform.parent.name == "Area1 (1)" || gameObject.transform.parent.name == "Area1 (2)")
                {
                    transform.parent.gameObject.SetActive(false);
                    stop = true;
                }
                else
                {
                    stop = true;
                }
            }

        }

    }

    public void UnlockNextArea()
    {
        next_Area.SetActive(true);
    }

    public void UnlockPrevArea()
    {
        prev_Area.SetActive(true);
    }

    public void HideCurrentArea()
    {
        current_Area.SetActive(false);
    }

    public void HidePrevArea()
    {
        prev_Area.SetActive(false);
    }
    public void HideNextArea()
    {
        next_Area.SetActive(false);
    }
}
