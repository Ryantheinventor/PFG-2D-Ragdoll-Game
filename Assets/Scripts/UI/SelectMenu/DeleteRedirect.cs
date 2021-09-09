using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteRedirect : MonoBehaviour
{
    public virtual void OnDelete() 
    {
        Destroy(gameObject);
    }
}
