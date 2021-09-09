using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public float thresholdVelocity = 10f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > thresholdVelocity)
        {
            foreach (Collider2D c in Physics2D.OverlapCircleAll(transform.position, 2))
            {
                if (c.attachedRigidbody)
                    c.attachedRigidbody.AddForce((c.attachedRigidbody.position - (Vector2)transform.position).normalized * 10, ForceMode2D.Impulse);
            }
            GameObject explosionFab = Resources.Load<GameObject>("Effects/Explosion");
            for (int i = 0; i < 5; i++) 
            {
                Instantiate(explosionFab).transform.position = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0).normalized * Random.Range(0f, 1f) + transform.position;
            }
            Destroy(gameObject);
        }
    }
}
