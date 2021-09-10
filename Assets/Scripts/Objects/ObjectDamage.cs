using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamage : MonoBehaviour
{
    public float speedForDamage = 12f;
    public string damageStatesFileName = "";
    protected Sprite[] damageStates;
    protected SpriteRenderer sRenderer;
    protected int curState = 0;
    public float totalV = 0;
    public bool destroyAtMaxDamage = true;
    public bool maxDamage = false;
    private void Start()
    {
        damageStates = Resources.LoadAll<Sprite>("objects/" + damageStatesFileName);
        sRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        curState = 0;
        float rV = totalV;
        if (collision.relativeVelocity.magnitude > 10) 
        {
            rV = totalV += collision.relativeVelocity.magnitude;
        }
        while (rV > speedForDamage) 
        {
            curState++;
            rV -= speedForDamage;
        }
        if (curState > damageStates.Length - 1 && destroyAtMaxDamage) 
        {
            Destroy(gameObject);
        }
        else
        {
            if (curState > damageStates.Length - 1)
            {
                maxDamage = true;
                curState = damageStates.Length - 1;
            }
            sRenderer.sprite = damageStates[curState];
        }
    }
}
