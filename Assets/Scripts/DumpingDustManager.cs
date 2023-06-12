using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpingDustManager : MonoBehaviour
{
    public GameObject[] Dusts;

    public bool DestroyOBJ;
    public void DumpDustOf(int index)
    {
        Dusts[index].SetActive(true);
    }
    private void FixedUpdate()
    {
        if (DestroyOBJ)
        {
            GetDestroyed();
        }
    }

    public void GetDestroyed()
    {
        Destroy(gameObject);
    }
}
