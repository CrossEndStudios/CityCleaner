using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public int DustIndex;

    float MovementSpeed;
    public float ShrinkingSpeed;

    public Rigidbody rb;

    

    Vector3 oriScale;

    public bool StartSuck;
    private void Start()
    {
        this.enabled = false;

        oriScale = transform.localScale;
        //deActivate();
    }
   
    public int GetIndex()
    {
        return DustIndex;
    }
    public void activate()
    {
        rb.isKinematic = false;
    }
    public void deActivate()
    {
        rb.isKinematic = true;
    }
    public void Eject()
    {
        transform.localScale = oriScale;
    }

    Transform vaccumepoint;
    PlayerMovement _player;
    Vector3 DecScale = new Vector3(0.01f, 0.01f, 0.01f);
    public void GetSucked(float speed,Transform _vaccumepoint,PlayerMovement player)
    {
        MovementSpeed = speed;
        StartSuck = true;
        _player = player;
        vaccumepoint = _vaccumepoint;
    }
    private void FixedUpdate()
    {
        if (StartSuck)
        {
            Vector3 scale = transform.localScale;


            if (Vector3.Distance(transform.position, vaccumepoint.position) > 0.2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, vaccumepoint.position, MovementSpeed * Time.deltaTime);

                scale = Vector3.MoveTowards(scale, DecScale, ShrinkingSpeed * Time.deltaTime);
                transform.localScale = scale;

            }
            else
            {
                _player.SuckedCollectables(this);
                this.enabled = false;

            }
        }
    }

    
}
