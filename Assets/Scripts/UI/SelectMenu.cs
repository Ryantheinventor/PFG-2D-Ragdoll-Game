using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectMenu : MonoBehaviour
{
    public static GameObject curSelected;


    //TODO the object should be created when an object is selected and destroyed(by itself) when an object is deselected
    private void Start()
    {
        //start animating from off screen to on screen
        StaticPlayerInput.PInput.currentActionMap["Click"].started += OnClick;
    }

    private void OnClick(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!EventSystem.current.IsPointerOverGameObject()) 
        {
            CloseMenu();
        }
    }

    private void Update()
    {
        if (!curSelected) 
        {
            CloseMenu();
        }
    }

    private void CloseMenu() 
    {
        //animate window sliding off screen the delete object
    }

}
