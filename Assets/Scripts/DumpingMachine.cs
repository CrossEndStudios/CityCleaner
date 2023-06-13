using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpingMachine : MonoBehaviour
{
    public Transform EjectutionPos;

    public DumpingDustManager Dustprefab;

    public GameObject[] GarbageBags;
    int BagIndex;
    public int garbageCapacity;
    int DustCollected;
    public int TotalDustCollected;

    private void Start()
    {
        foreach (var b in GarbageBags)
        {
            b.SetActive(false);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartDumping();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopDumping();
        }
    }
    float dustindexTimer;
    void StartDumping()
    {
        if (GameData.collected.Count > 0)
        {
            int colIndex = GameData.collected[0];

            DumpingDustManager dust = Instantiate(Dustprefab, EjectutionPos.position, Quaternion.identity);
            //dust.transform.SetParent(EjectutionPos.transform);

            dust.DumpDustOf(colIndex);
            GameData.collected.RemoveAt(0);

            DustCollected++;
            TotalDustCollected++;
            if (DustCollected >= garbageCapacity)
            {
                if(BagIndex < GarbageBags.Length)
                {
                    BagIndex++;
                }
                else
                {
                    BagIndex = 0 ;
                }

                GameObject bag = GarbageBags[BagIndex];
                bag.SetActive(true);
                Gamemanager.instance.Truck.AddGarbageBags(bag);

                DustCollected = 0;
            }


        }
        else
        {
            Debug.Log("DustClear");
        }
    }
    void StopDumping()
    {

    }
}
