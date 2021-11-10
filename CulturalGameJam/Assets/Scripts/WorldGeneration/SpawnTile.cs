using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{

    public GameObject[] objs;
    // Start is called before the first frame update
    void Start()
    {
        int rnd = Random.Range(0, (objs.Length - 1));
        //Debug.Log("Length: " + objs.Length.ToString() + ", " + rnd.ToString());
        GameObject instancia = (GameObject)Instantiate(objs[rnd], transform.position, Quaternion.identity);
        instancia.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
