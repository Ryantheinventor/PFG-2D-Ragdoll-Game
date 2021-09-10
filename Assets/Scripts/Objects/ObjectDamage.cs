using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamage : MonoBehaviour
{
    public float speedForDamage = 12f;
    public string damageStatesFileName = "";
    private Sprite[] damageStates;
    private SpriteRenderer sRenderer;
    private int curState = 0;
    public float totalV = 0;
    private void Start()
    {
        damageStates = Resources.LoadAll<Sprite>("objects/" + damageStatesFileName);
        sRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        curState = 0;
        float rV = totalV += collision.relativeVelocity.magnitude;
        while (rV > speedForDamage) 
        {
            curState++;
            rV -= speedForDamage;
        }
        if (curState > damageStates.Length - 1) 
        {
            Destroy(gameObject);
        }
        else
        {
            sRenderer.sprite = damageStates[curState];
        }

    }
}
