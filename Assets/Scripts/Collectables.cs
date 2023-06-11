using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    float MovementSpeed;
    public float ShrinkingSpeed;


    public void GetSucked(float speed,Transform vaccumepoint,PlayerMovement player)
    {
        MovementSpeed = speed;
        StartCoroutine(MoveToVaccume(vaccumepoint, player));
    }

    IEnumerator MoveToVaccume(Transform vaccumepoint,PlayerMovement player)
    {
        while (Vector3.Distance(transform.position,vaccumepoint.position) > 0.2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, vaccumepoint.position, MovementSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
        player.SuckedCollectables(this);
    }
}
