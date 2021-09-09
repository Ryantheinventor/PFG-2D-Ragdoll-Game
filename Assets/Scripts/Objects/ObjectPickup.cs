using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPickup : MonoBehaviour
{
    public float forceMultiplier = 10f;
    public static bool pickupEnabled = true;
    private static Rigidbody2D objectHeld;
    private Camera cam;
    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        StaticPlayerInput.PInput.currentActionMap["Click"].started += OnClick;
    }

    private void OnDisable()
    {
        StaticPlayerInput.PInput.currentActionMap["Click"].started -= OnClick;
    }

    private void OnClick(InputAction.CallbackContext obj)
    {
        if (!objectHeld)
        {
            GameObject target = PickingRaycast.TargetedObject;
            if (pickupEnabled && target)
            {
                if (target.GetComponent<PickupRedirect>())
                {
                    objectHeld = target.GetComponent<PickupRedirect>().redirectTo.GetComponent<Rigidbody2D>();
                }
                else 
                {
                    objectHeld = target.GetComponent<Rigidbody2D>();
                }

            }
        }
        else 
        {
            objectHeld = null;
        }
    }

    private void FixedUpdate()
    {
        if (objectHeld) 
        {
            if (!pickupEnabled) 
            {
                objectHeld = null;
            }

            Vector2 force = objectHeld.position - (Vector2)cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            objectHeld.velocity = -force * forceMultiplier - 0.5f * force * Time.fixedDeltaTime;

        }
    }

}
