using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PickingRaycast : MonoBehaviour
{
    private static GameObject curTarget = null;
    public static GameObject TargetedObject 
    {
        get => curTarget;
    }
    private Camera cam;
    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector3.forward);
        if (hit.collider && !EventSystem.current.IsPointerOverGameObject())
        {
            curTarget = hit.collider.gameObject;
        }
        else 
        {
            curTarget = null;
        }
    }
}
