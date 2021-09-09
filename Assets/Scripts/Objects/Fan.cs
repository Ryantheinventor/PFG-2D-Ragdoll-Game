using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public float fanForce = 10f;
    List<Rigidbody2D> affectedRigidbodys = new List<Rigidbody2D>();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody && !collision.isTrigger) 
        {
            if (!affectedRigidbodys.Contains(collision.attachedRigidbody))
            {
                affectedRigidbodys.Add(collision.attachedRigidbody);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody && !collision.isTrigger)
        {
            if (affectedRigidbodys.Contains(collision.attachedRigidbody))
            {
                affectedRigidbodys.Remove(collision.attachedRigidbody);
            }
        }
    }

    private void FixedUpdate()
    {
        foreach (Rigidbody2D rb in affectedRigidbodys) 
        {
            rb.velocity += (Vector2)(-transform.right * fanForce * Time.deltaTime / (rb.position - (Vector2)transform.position).magnitude);
        }
    }

}
