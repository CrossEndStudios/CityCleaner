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

    public enum TruckMovements
    {
        Parked,
        Moving
    }

    [Header("Drone Properties")]
    public Transform Drone;
    public float DroneMovementSpeed;
    public Transform DroneRestPos;
    public Transform DroneHookPoint;
    public List<GameObject> GarbageBags = new List<GameObject>();
    public DroneProp DroneState;
    public DronesSenses DroneSense;

    public float RotationSpeed;

    Vector3 DroneHookingPosition;
    Vector3 DroneTarget;

    [Space]
    [Header("Truck Properties")]
    public Transform SeatPos;
    public Transform GatePos;
    public TruckMovements TruckState;
    public float TruckMoveSpeed;
    public Transform Target;
    public List<Transform> RoadPoints = new List<Transform>();
    public int RoadPointsindex;

    [Space]
    [Header("Player Properties")]
    public PlayerMovement player;



    private void Start()
    {
        DroneRestPos.position += new Vector3(0, 2, 0);

        Drone.transform.position = DroneRestPos.position;

        DroneState = DroneProp.NotAssigned;
        DroneSense = DronesSenses.GarbageNotFound;


        player.GetInTruck(SeatPos);

    }


    public void AddGarbageBags(GameObject bag)
    {
        GarbageBags.Add(bag);
    }
    private void FixedUpdate()
    {
        CheckForGarbage();

        if (TruckState == TruckMovements.Moving)
        {
            //Truck Movements
            if(Vector3.Distance(transform.position,Target.position) > 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, Target.position, TruckMoveSpeed * Time.fixedDeltaTime);
                transform.LookAt(Target);
            }
            else
            {
                if(RoadPointsindex < RoadPoints.Count-1)
                {
                    RoadPointsindex++;
                    Target = RoadPoints[RoadPointsindex];
                }
                else
                {
                    TruckState = TruckMovements.Parked;

                    player.GetOutTruck(GatePos);

                    return;
                }
            }
        }
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

                    DroneTarget = DroneHookingPosition;

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

                    DroneTarget = DroneRestPos.position;

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

            Vector3 direction = DroneTarget - Drone.transform.position;
            direction.y = 0f;

            Quaternion desiredRotation = Quaternion.LookRotation(direction);

            Drone.transform.rotation = Quaternion.Lerp(Drone.transform.rotation, desiredRotation, RotationSpeed * Time.deltaTime);

        }
        else
        {
            DroneSense = DronesSenses.GarbageNotFound;
            DroneTarget = Vector3.zero;
        }
    }

    public void MoveToNextHouse(Transform[] Points)
    {
        RoadPoints.Clear();

        for (int i = 0; i < Points.Length; i++)
        {
            Vector3 P_pos = Points[i].transform.position;
            P_pos.y = transform.position.y;
            Points[i].transform.position = P_pos;

            RoadPoints.Add(Points[i]);
        }
        RoadPointsindex = 0;
        Target = RoadPoints[RoadPointsindex];
        TruckState = TruckMovements.Moving;

    }


}
