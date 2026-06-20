using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Gravity : MonoBehaviour
{
    [Header("Gravity")]
    //don't overcomplicate things and just take a nice looking gravitational constant
    public float gravitationalConstant = 100f;
    public float planetMass = 50f;

    void OnTriggerStay2D(Collider2D col)
    {
        //get rigidbody from spaceship
        Rigidbody2D rb = col.GetComponent<Rigidbody2D>();

        //calculate direction and distance
        Vector2 planetPos = transform.position;
        Vector2 direction = planetPos - rb.position;

        float distance = direction.magnitude;

        //newtons law of gravity
        float gravitationalForce = gravitationalConstant * rb.mass * planetMass / (distance * distance);
        
        //normalize direction and add force
        rb.AddForce(direction.normalized * gravitationalForce);
    }
}