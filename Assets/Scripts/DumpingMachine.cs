using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpingMachine : MonoBehaviour
{
    public Transform EjectutionPos;

    public DumpingDustManager Dustprefab;
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
        /*if (dustindexTimer <= 0)
        {
            
            dustindexTimer = 0.00f;
        }
        else
        {
            dustindexTimer -= Time.deltaTime;
        }*/
        if (GameData.collected.Count > 0)
        {
            int colIndex = GameData.collected[0];

            DumpingDustManager dust = Instantiate(Dustprefab, EjectutionPos.position, Quaternion.identity);
            //dust.transform.SetParent(EjectutionPos.transform);

            dust.DumpDustOf(colIndex);
            GameData.collected.RemoveAt(0);
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
