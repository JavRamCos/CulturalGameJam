using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSidedSpike : MonoBehaviour {
    [SerializeField] private GameObject spikePos;

    public Vector2 GetSpikePosition() { 
        return new Vector2(spikePos.transform.position.x,spikePos.transform.position.y);
    }
}
