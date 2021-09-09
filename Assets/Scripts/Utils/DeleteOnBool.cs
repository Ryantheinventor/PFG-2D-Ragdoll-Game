using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnBool : MonoBehaviour
{
    public bool delete = false;

    private void Update()
    {
        if (delete) 
        {
            Destroy(gameObject);
        }
    }
}
