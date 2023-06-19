using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public Transform[] TruckWaypoint;


    public GameObject NextlevelWall;
    Transform TruckStandgingPos;

    public bool HouseCleared;

    private void Start()
    {
        foreach (var p in TruckWaypoint)
        {
            p.GetComponent<MeshRenderer>().enabled = false;
        }
    }


    public Transform GetTruckPos()
    {
        TruckStandgingPos = TruckWaypoint[TruckWaypoint.Length];


        return TruckStandgingPos;
    }
    
}
