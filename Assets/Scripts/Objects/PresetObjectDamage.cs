using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetObjectDamage : ObjectDamage
{
    private void Start()
    {
        
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
