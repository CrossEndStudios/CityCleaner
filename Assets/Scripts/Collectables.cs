using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    float MovementSpeed;
    public float ShrinkingSpeed;

    public Rigidbody rb;

    Vector3 oriScale;
    private void Start()
    {
        oriScale = transform.localScale;

        rb = GetComponent<Rigidbody>();
        //deActivate();
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
    public void GetSucked(float speed,Transform vaccumepoint,PlayerMovement player)
    {
        MovementSpeed = speed;
        StartCoroutine(MoveToVaccume(vaccumepoint, player));
    }

    IEnumerator MoveToVaccume(Transform vaccumepoint,PlayerMovement player)
    {
        Vector3 scale = transform.localScale;
        Vector3 DecScale = new Vector3(0.01f, 0.01f, 0.01f);

        while (Vector3.Distance(transform.position,vaccumepoint.position) > 0.2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, vaccumepoint.position, MovementSpeed * Time.deltaTime);

            scale = transform.localScale;
            scale = Vector3.MoveTowards(scale, DecScale, ShrinkingSpeed * Time.deltaTime);
            transform.localScale = scale;

            yield return new WaitForSeconds(0.01f);
        }
        player.SuckedCollectables(this);
    }
}
