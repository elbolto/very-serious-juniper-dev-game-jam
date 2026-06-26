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
        if (rb == null) 
        {
            return;
        }

        // turn of damping when in gravity fields. Otherwise it's not fun.
        CharacterMovement player = col.gameObject.GetComponent<CharacterMovement>();
        if (player != null)
        {
            player.damping = false;
        }

        //calculate direction and distance
        Vector2 direction = (Vector2)transform.parent.position - rb.position;

        float distance = direction.magnitude;

        //newtons law of gravity
        float gravitationalForce = gravitationalConstant * rb.mass * planetMass/distance;

        //normalize direction and add force
        rb.AddForce(direction.normalized * gravitationalForce);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        // turn of damping when in gravity fields. Otherwise it's not fun.
        CharacterMovement player = col.gameObject.GetComponent<CharacterMovement>();
        if (player != null)
        {
            player.damping = true;
            Debug.Log("Turn on damping");
        }
    }
}