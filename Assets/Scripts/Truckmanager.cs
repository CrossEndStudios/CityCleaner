using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truckmanager : MonoBehaviour
{
    public enum DroneProp
    {
        Assigned,
        NotAssigned
    }
    public enum DronesSenses
    {
        GarbageNotFound,
        GarbageFound,
        ReadyToHook,
        Hooked
    }

    [Header("Drone Properties")]
    public Transform Drone;
    public float DroneMovementSpeed;
    public Transform DroneRestPos;
    public Transform DroneHookPoint;
    public List<GameObject> GarbageBags = new List<GameObject>();
    public DroneProp DroneState;
    public DronesSenses DroneSense;
    Vector3 DroneHookingPosition;


    private void Start()
    {

        DroneRestPos.position += new Vector3(0, 2, 0);

        Drone.transform.position = DroneRestPos.position;

        DroneState = DroneProp.NotAssigned;
        DroneSense = DronesSenses.GarbageNotFound;
    }

    public void AddGarbageBags(GameObject bag)
    {
        GarbageBags.Add(bag);
    }

    private void FixedUpdate()
    {
        CheckForGarbage();
    }
    void CheckForGarbage()
    {
        if(GarbageBags.Count > 0 )
        {
            if (DroneState != DroneProp.Assigned)
                DroneSense = DronesSenses.GarbageFound;

            DroneHookingPosition = GarbageBags[0].transform.position + new Vector3(0,2,0);

            if (DroneSense == DronesSenses.GarbageFound && DroneState != DroneProp.Assigned)
            {
                if(Drone.position != DroneHookingPosition && DroneSense != DronesSenses.ReadyToHook)
                {
                    Drone.position = Vector3.MoveTowards(Drone.position, DroneHookingPosition, DroneMovementSpeed * Time.fixedDeltaTime);

                    if (Drone.position == DroneHookingPosition)
                    {
                        DroneSense = DronesSenses.ReadyToHook;
                    }
                }
                else if (DroneState != DroneProp.Assigned)
                {
                    GarbageBags[0].transform.SetParent(null);
                    GarbageBags[0].transform.position = DroneHookPoint.transform.position;
                    GarbageBags[0].transform.SetParent(DroneHookPoint.transform);

                    DroneState = DroneProp.Assigned;
                    DroneSense = DronesSenses.Hooked;
                }
            }
            else if(DroneSense == DronesSenses.Hooked && DroneState == DroneProp.Assigned)
            {
                if (Drone.position != DroneRestPos.position)
                {
                    Drone.position = Vector3.MoveTowards(Drone.position, DroneRestPos.position, DroneMovementSpeed * Time.fixedDeltaTime);

                    if (Drone.position == DroneRestPos.position)
                    {
                        GameObject Bag = GarbageBags[0];
                        GarbageBags.Remove(Bag);
                        Destroy(Bag);

                        DroneState = DroneProp.NotAssigned;
                        DroneSense = DronesSenses.ReadyToHook;
                    }
                }
            }
        }
        else
        {
            DroneSense = DronesSenses.GarbageNotFound;
        }
    }



}
