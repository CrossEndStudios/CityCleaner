using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    // Start is called before the first frame update

    public static Gamemanager instance;


    public Truckmanager Truck;

    private void Awake()
    {
        if(instance == null) { instance = this; }
    }

    void Start()
    {
        GameData.collected = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
