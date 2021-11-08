using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoom : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] rooms;

    private int direccion;
    public float moveAmount = 10f;

    private float time;
    public float startTime = 0.25f;

    public float minX;
    public float maxX;
    public float minY;
    public bool stop;

    public LayerMask room;

    private int dCount;

    // Start is called before the first frame update
    void Start()
    {
        int rndSpawnPoint = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[rndSpawnPoint].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

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
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direccion = Random.Range(1, 6);
                if (direccion == 3)
                {
                    direccion = 2;
                }
                if (direccion == 4)
                {
                    direccion = 5;
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
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direccion = Random.Range(3, 6);
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
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetection[0].GetComponent<RoomType>().DestroyRoom();

                        int randomBottom = Random.Range(1, 4);
                        if (randomBottom == 2)
                        {
                            randomBottom = 1;
                        }
                        Instantiate(rooms[randomBottom], transform.position, Quaternion.identity);
                    }


                }


                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direccion = Random.Range(1, 5);
            }
            else
            {
                stop = true;
            }

        }

    }
}
