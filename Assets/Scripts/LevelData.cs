using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public Transform[] TruckWaypoint;


    public GameObject NextlevelWall;
    Transform TruckStandgingPos;

    public bool HouseCleared;


    public void OpenNextLevel()
    {
        NextlevelWall.SetActive(false);
    }

    public Transform GetTruckPos()
    {
        TruckStandgingPos = TruckWaypoint[TruckWaypoint.Length];


        return TruckStandgingPos;
    }
    
}
