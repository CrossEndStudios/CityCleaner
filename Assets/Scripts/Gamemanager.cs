using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    // Start is called before the first frame update

    public static Gamemanager instance;


    public Truckmanager Truck;

    public int CityIndex;

    public LevelData[] Levels;
    public int LevelIndex;
    [Space]
    [Header("Garbage")]
    public int TotalDusts;
    public int DustCollected;
    private void Awake()
    {
        if(instance == null) { instance = this; }
    }

    void Start()
    {
        GameData.collected = new List<int>();

        Truck.MoveToNextHouse(Levels[LevelIndex].TruckWaypoint);

    }

    public void AddDust(int amt)
    {
        DustCollected += amt;
    }
    public void CalculateTotalDust(int amt)
    {
        TotalDusts += amt;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
