using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float Vertical_Inp;
    float Horizontal_Inp;
    Vector3 Direction;
    public float MovementSpeed;
    public float RotationSpeed;

    Rigidbody rb;

    [Range(0,10)]
    public float ControlDimDelay;

    float Ver_Cont;
    float Hor_Cont;

    [Space]
    [Header("Vaccume Variables")]
    public Transform VaccumePoint;
    public float SuckingSpeed;

    [Space]
    public Transform EjectutionPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    float dustindexTimer;

    void Update()
    {
        Vertical_Inp = SimpleInput.GetAxis("Vertical");
        Horizontal_Inp = SimpleInput.GetAxis("Horizontal");


        Ver_Cont = Mathf.Lerp(Ver_Cont, Vertical_Inp, ControlDimDelay * Time.deltaTime);
        Hor_Cont = Mathf.Lerp(Hor_Cont, Horizontal_Inp, ControlDimDelay * Time.deltaTime);


        if (Input.GetKey(KeyCode.P) )
        {
            if( dustindexTimer <= 0)
            {
                 if (GameData.collected.Count > 0)
                 {
                     Collectables col = GameData.collected[0];
                     col.gameObject.transform.position = EjectutionPos.position;
                     col.gameObject.SetActive(true);
                    col.Eject();
                     GameData.collected.Remove(col);
                 }
                 else
                 {
                     Debug.Log("DustClear");
                 }
                dustindexTimer = 0.1f;
            }
            else
            {
                dustindexTimer -= Time.deltaTime;
            }
        }
        

    }
    private void FixedUpdate()
    {
        Controls();
    }
    void Controls()
    {
        Direction = new Vector3(Hor_Cont, -20f * Time.fixedDeltaTime, Ver_Cont);

        rb.velocity = (Direction * MovementSpeed) * Time.fixedDeltaTime;

        Vector3 LookVector = Direction;
        LookVector.y = 0;

        if (LookVector != Vector3.zero)
        {
            transform.forward = Vector3.MoveTowards(transform.forward,LookVector, RotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Collectables>())
        {
            Collectables collect = other.GetComponent<Collectables>();

            collect.activate();
            
            collect.GetSucked(SuckingSpeed, VaccumePoint,this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Collectables>())
        {
            Collectables collect = other.GetComponent<Collectables>();
            collect.deActivate();
        }
    }
    public void SuckedCollectables(Collectables coll)
    {
        GameData.collected.Add(coll);
        coll.gameObject.SetActive(false);
    }
}
