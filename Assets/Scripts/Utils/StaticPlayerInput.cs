using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class StaticPlayerInput : MonoBehaviour
{
    private static PlayerInput pInput = null;
    public static PlayerInput PInput 
    {
        get 
        {
            if (!pInput)
            {
                pInput = FindObjectOfType<PlayerInput>();
            }
            return pInput;
        }
    }
}
