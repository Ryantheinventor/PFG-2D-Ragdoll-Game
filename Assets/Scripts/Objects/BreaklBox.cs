using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreaklBox : MonoBehaviour
{
    private ObjectDamage od;
    private void Start()
    {
        od = GetComponent<ObjectDamage>();
    }

    private void Update()
    {
        if (od.maxDamage) 
        {
            Achievements.NewAchievement("Whoops", "\"Whoops\"!? I liked that box and you just broke it. This is why I don't like you.");
            Destroy(gameObject);
        }
    }
}
