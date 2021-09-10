using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerArms : MonoBehaviour
{
    private static bool armsActive = false;
    public Collider2D head;
    public Rigidbody2D leftArm;
    public Rigidbody2D rightArm;
    public SpriteRenderer leftWeapon;
    public SpriteRenderer rightWeapon;
    public GameObject projectile;
    public Transform leftPSpawn;
    public Transform rightPSpawn;
    public float projectileSpeed = 10f;
    public float armAcceleration = 5f;
    private Camera cam;
    private bool useRightArm = false;
    private static PlayerArms instance = null;

    private void Start()
    {
        instance = this;
        Physics2D.IgnoreCollision(head, leftArm.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(head, rightArm.GetComponent<Collider2D>());
        cam = Camera.main;
    }

    public static void ActivatePlayerArms()
    {
        if (!armsActive) 
        {
            armsActive = true;
            StaticPlayerInput.PInput.currentActionMap["Click"].started += OnFire;
        }
    }

    

    public static void DisablePlayerArms()
    {
        if (armsActive) 
        {
            armsActive = false;
            StaticPlayerInput.PInput.currentActionMap["Click"].started -= OnFire;
        }
    }

    //fire weapon
    public static void OnFire(InputAction.CallbackContext obj)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (instance.useRightArm)
            {
                GameObject newProjectile = Instantiate(instance.projectile);
                newProjectile.GetComponent<Rigidbody2D>().velocity = -instance.rightArm.transform.up * instance.projectileSpeed;
                newProjectile.transform.position = instance.rightPSpawn.transform.position;
            }
            else
            {
                GameObject newProjectile = Instantiate(instance.projectile);
                newProjectile.GetComponent<Rigidbody2D>().velocity = -instance.leftArm.transform.up * instance.projectileSpeed;
                newProjectile.transform.position = instance.leftPSpawn.transform.position;
            }
        }
    }

    //aiming
    private void FixedUpdate()
    {
        Debug.Log(Mouse.current.position.ReadValue());
        if (armsActive)
        {
            Vector2 mouseWorldPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            #region aiming
            if (Vector2.Distance(mouseWorldPos, leftArm.position) > Vector2.Distance(mouseWorldPos, rightArm.position))
            {
                rightArm.gravityScale = 0;
                leftArm.gravityScale = 1;
                leftWeapon.enabled = false;
                rightWeapon.enabled = true;
                Debug.Log("Right Arm");
                //use right arm
                useRightArm = true;
                Vector2 rrPoint = (Vector2)(-rightArm.transform.up + rightArm.transform.right) * 0.2f + rightArm.position;//cw rotation needed
                Vector2 lrPoint = (Vector2)(-rightArm.transform.up - rightArm.transform.right) * 0.2f + rightArm.position;//ccw rotation needed
                if (Vector2.Distance(mouseWorldPos, rrPoint) > Vector2.Distance(mouseWorldPos, lrPoint))
                {
                    //rotate cw

                    rightArm.angularVelocity -= armAcceleration;
                    Debug.DrawLine(mouseWorldPos, rrPoint, Color.red);
                }
                else
                {
                    //rotate ccw
                    rightArm.angularVelocity += armAcceleration;
                    Debug.DrawLine(mouseWorldPos, lrPoint);
                }
            }
            else
            {
                rightArm.gravityScale = 1;
                leftArm.gravityScale = 0;
                leftWeapon.enabled = true;
                rightWeapon.enabled = false;
                //use left arm
                useRightArm = false;
                Vector2 rlPoint = (Vector2)(-leftArm.transform.up + leftArm.transform.right) * 0.2f + leftArm.position;//cw rotation needed
                Vector2 llPoint = (Vector2)(-leftArm.transform.up - leftArm.transform.right) * 0.2f + leftArm.position;//ccw rotation needed
                if (Vector2.Distance(mouseWorldPos, rlPoint) > Vector2.Distance(mouseWorldPos, llPoint))
                {
                    //rotate cw

                    leftArm.angularVelocity -= armAcceleration;
                    Debug.DrawLine(mouseWorldPos, rlPoint, Color.red);
                }
                else
                {
                    //rotate ccw
                    leftArm.angularVelocity += armAcceleration;
                    Debug.DrawLine(mouseWorldPos, llPoint);
                }
            }
            #endregion


        }
        else 
        {
            rightArm.gravityScale = 1;
            leftArm.gravityScale = 1;
            leftWeapon.enabled = false;
            rightWeapon.enabled = false;
        }
    }
}
