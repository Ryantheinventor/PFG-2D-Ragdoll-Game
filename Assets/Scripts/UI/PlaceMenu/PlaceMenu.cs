using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMenu : MonoBehaviour
{
    public bool menuClosed = false;
    public static PlaceMenu instance = null;
    private static Transform canvas;
    private static GameObject myPrefab;
    public static int spawnedObjects = 0;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (menuClosed)
        {
            Destroy(gameObject);
        }
        if (spawnedObjects == 100)
        {
            Achievements.NewAchievement("Behold! My stuff:", "Spawned 100 objects.");
        }
        if (spawnedObjects == 200)
        {
            Achievements.NewAchievement("You still need more?", "Spawned 200 objects.");
        }
        if (spawnedObjects == 500)
        {
            Achievements.NewAchievement("Wormhole?", "NO seriously where is all this stuff coming from.");
        }
    }

    private void CloseMenu()
    {
        if (instance == this)
        {
            instance = null;
            GetComponent<Animator>().Play("Close");
        }
    }

    public static void CloseCurMenu()
    {
        if (instance)
            instance.CloseMenu();
    }

    public static void OpenMenu() 
    {
        if (!canvas) 
        {
            canvas = FindObjectOfType<Canvas>().transform;
        }
        if (!instance) 
        {
            if (!myPrefab) 
            {
                myPrefab = Resources.Load<GameObject>("UI/PlaceMenu");
            }
            (Instantiate(myPrefab, myPrefab.transform.position, myPrefab.transform.rotation) as GameObject).transform.SetParent(canvas, false);
        }
            
    }


}
