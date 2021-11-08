using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSidedSpike : MonoBehaviour
{
    [SerializeField] private GameObject positionOne;
    [SerializeField] private GameObject positionTwo;
    [SerializeField] private GameObject positionThree;
    [SerializeField] private GameObject positionFour;

    private GameObject[] spikesPositions;

    private void Awake() {
        spikesPositions = new GameObject[4] { positionOne, positionTwo, positionThree, positionFour };
    }

    public Vector2 GetNearestSpikePosition(Vector2 playerPos) {
        float minDistance = 1000f;
        Vector2 closest = new Vector2(0,0);
        foreach(GameObject pos in spikesPositions) {
            float distance = Mathf.Sqrt(Mathf.Pow(pos.transform.position.x,2) + Mathf.Pow(pos.transform.position.y,2));
            if(distance <= minDistance) { 
                closest = new Vector2(pos.transform.position.x,pos.transform.position.y); 
            }
        }
        return closest;
    }
}