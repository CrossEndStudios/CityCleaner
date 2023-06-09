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
        TargetOffset = (Target.position - transform.position);
    }


    

    Vector3 FollowPos;
    void Follow()
    {
        FollowPos = Target.position - TargetOffset;

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
