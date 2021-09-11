using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dismemberment : MonoBehaviour
{
    public HingeJoint2D jointToBreak;
    public ObjectDamage objectDamage;
    public bool killsPlayer;

    private void Update()
    {
        if (objectDamage.maxDamage)
        {
            
            Destroy(jointToBreak);
            Destroy(this);
            if (killsPlayer)
            {
                Achievements.NewAchievement("My head!!", "Hmm, You don't need that one right?");
                RagdollUpright.isDead = true;
            }
            else 
            {
                Achievements.NewAchievement("<i>Snap</i>", "You did't need that anyway...");
            }
        }
    }


}
