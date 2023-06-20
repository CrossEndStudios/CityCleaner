using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustPlane : MonoBehaviour
{
    public GameObject cubePrefab;

    int planeWidth;
    int planeHeight;
    public float cubeSize;


    public float min_Thr;
    public float max_Thr;

    public float threshold;

    public Vector3 PlanePos;
    GameObject DustHolder;


    public GameObject[] extraStuffs;
    [Range(0, 100)] public float ExtraStuffSpawnChance;

    public bool setup;

    [HideInInspector]
    public List<GameObject> Dusts = new List<GameObject>();

    public int TotalDustparticles;

    void Start()
    {
        /*if (setup)
        {
            SetupDusts();
        }*/
    }
    float[,] noisemap;
    public void SetupDusts()
    {
        threshold = Random.Range(min_Thr, max_Thr);

        transform.GetComponent<MeshRenderer>().enabled = false;

        DustHolder = new GameObject("DustHolder");
        DustHolder.transform.parent = transform;
        PlanePos = transform.position;

        //Get Values
        /*planeHeight = (int)(transform.localScale.z * multipliar);
        planeWidth = (int)(transform.localScale.x * multipliar);*/

        planeHeight = (int)(transform.lossyScale.z * 10);
        planeWidth = (int)(transform.lossyScale.x * 10);

        PlanePos.x -= (transform.lossyScale.x * 5) - cubeSize;
        PlanePos.z -= (transform.lossyScale.z * 5) - cubeSize;

        // Generate noisemap
        noisemap = new float[planeWidth, planeHeight];
        for (int i = 0; i < planeWidth; i++)
        {
            for (int j = 0; j < planeHeight; j++)
            {
                float value = Mathf.PerlinNoise(i * Random.Range(0.1f, 0.15f), j * Random.Range(0.1f, 0.15f));
                //float value = Mathf.PerlinNoise(i * .1f, j * .1f);
                noisemap[i, j] = value;
            }
        }

        SpawnDusts();
    }
    void SpawnDusts()
    {
        // Instantiate cubes

        int XIteration = (int)(planeWidth / cubeSize);
        int YIteration = (int)(planeHeight / cubeSize);

        for (int i = 0; i < XIteration; i++)
        {
            for (int j = 0; j < YIteration; j++)
            {
                if (noisemap[i, j] > threshold)
                {
                    Vector3 position = new Vector3((i * cubeSize) + PlanePos.x, PlanePos.y, (j * cubeSize) + PlanePos.z);


                    GameObject dust;
                    if (Random.Range(1, 100) <= ExtraStuffSpawnChance)
                    {
                        dust = Instantiate(extraStuffs[0], position, Quaternion.identity, DustHolder.transform).gameObject;
                    }
                    else
                    {

                        dust = Instantiate(cubePrefab, position, Quaternion.identity, DustHolder.transform).gameObject;
                    }

                    Dusts.Add(dust);

                    TotalDustparticles++;
                }
            }
        }

        Gamemanager.instance.CalculateTotalDust(TotalDustparticles);
    }
    public void ReSetlevel()
    {
        TotalDustparticles = 0;
        foreach (var D in Dusts)
        {
            Destroy(D);
        }
        SpawnDusts();
    }
}
