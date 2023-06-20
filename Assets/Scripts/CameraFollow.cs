using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{





    public Transform Target;

    public float FollowSpeed;



    Vector3 TargetOffset;


    public bool LerpPosition;



    private void Start()
    {
        if(transform.parent != null)
        {
            transform.SetParent(null);
        }

        TargetOffset = (Target.position - transform.position);


    }

    Vector3 FollowPos;
    private void Update()
    {
        FollowPos = Target.position - TargetOffset;
    }
    void Follow()
    {
        

        transform.LookAt(Target);
        if (LerpPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, FollowPos, FollowSpeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.position = FollowPos;
        }
    }

    private void FixedUpdate()
    {
        Follow();
    }
   
}
