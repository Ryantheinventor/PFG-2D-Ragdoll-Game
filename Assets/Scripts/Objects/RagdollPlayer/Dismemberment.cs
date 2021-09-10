using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dismemberment : MonoBehaviour
{
    public HingeJoint2D jointToBreak;
    public ObjectDamage objectDamage;

    private void Update()
    {
        if (objectDamage.maxDamage)
        {
            Destroy(jointToBreak);
            Destroy(this);
        }
    }


}
