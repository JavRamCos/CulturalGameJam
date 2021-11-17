using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawn : MonoBehaviour
{

    public GameObject[] props;

    // Start is called before the first frame update
    void Start()
    {
        int rnd = Random.Range(0, props.Length);
        if (rnd > props.Length)
        {
            //Debug.Log("Simon aqui no va nada");
        }
        else
        {
            var newObj = Instantiate(props[rnd], transform.position, Quaternion.identity);
            newObj.transform.parent = gameObject.transform;
            
        }

    }

    // Update is called once per frame
    void Update()
    {

    }



}