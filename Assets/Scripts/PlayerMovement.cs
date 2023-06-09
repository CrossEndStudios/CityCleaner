using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float Vertical_Inp;
    float Horizontal_Inp;
    Vector3 Direction;
    public float MovementSpeed;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vertical_Inp = Input.GetAxis("Vertical");
        Horizontal_Inp = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        Controls();
    }
    void Controls()
    {
        Direction = new Vector3(Horizontal_Inp, -20f * Time.fixedDeltaTime, Vertical_Inp);

        rb.velocity = (Direction * MovementSpeed) * Time.fixedDeltaTime;

        Vector3 LookVector = Direction;
        LookVector.y = 0;

        if (LookVector != Vector3.zero)
        {
            transform.forward = LookVector;
        }
    }
}
