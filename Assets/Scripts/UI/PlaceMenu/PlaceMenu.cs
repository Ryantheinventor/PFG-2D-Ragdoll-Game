using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMenu : MonoBehaviour
{
    public bool menuClosed = false;
    private static PlaceMenu instance = null;

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
}
