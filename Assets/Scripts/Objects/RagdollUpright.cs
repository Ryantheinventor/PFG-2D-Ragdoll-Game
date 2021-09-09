using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollUpright : MonoBehaviour
{
    public GameObject torsoForcePoint;
    public GameObject headForcePoint;
    public float torsoForce = 1f;
    public float headForce = 1f;
    private Rigidbody2D torso;
    private Rigidbody2D head;
    private void Start()
    {
        torso = torsoForcePoint.transform.parent.GetComponent<Rigidbody2D>();
        head = headForcePoint.transform.parent.GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        foreach (Collider2D c in Physics2D.OverlapCircleAll(torsoForcePoint.transform.position, 1))
        {
            if (!c.transform.IsChildOf(transform))
            {
                torso.AddForceAtPosition(Vector2.down * torsoForce * Time.deltaTime, torsoForcePoint.transform.position);
                head.AddForceAtPosition(Vector2.up * headForce * Time.deltaTime, headForcePoint.transform.position);
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (Collider2D c in Physics2D.OverlapCircleAll(torsoForcePoint.transform.position, 1)) 
        {
            if (!c.transform.IsChildOf(transform)) 
            {
                Gizmos.color = Color.green;
                break;
            }
        }

        Gizmos.DrawWireSphere(torsoForcePoint.transform.position, 1);
    }


}
