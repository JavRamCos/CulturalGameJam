using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtrosRooms : MonoBehaviour
{

    public LayerMask room;
    public CreateRoom levelGen;
    public GameObject hp;

    private int count = 0;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider[] roomDetection = Physics.OverlapSphere(transform.position, 1, room, QueryTriggerInteraction.Ignore);

        if (roomDetection.Length == 0 && levelGen.stop == true)
        {
            int rnd = Random.Range(0, levelGen.rooms.Length);
            Instantiate(levelGen.rooms[rnd], transform.position, Quaternion.identity);
            count += 1;

            int randHP = Random.Range(0, 1);
            if(randHP == 0)
            {
                Instantiate(hp, new Vector2(transform.position.x, transform.position.y - 6.5f), Quaternion.identity);
                Debug.Log("HP");
            }
        }

    }
}
