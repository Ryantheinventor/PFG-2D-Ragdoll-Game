using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class MouseMode : MonoBehaviour
{
    public PointerMode curMode = PointerMode.Move;
    private GameObject selectMenuFab;
    private Canvas myCanvas;
    public enum PointerMode 
    {
        Select,
        Move,
        Place
    }

    private void Start()
    {
        myCanvas = transform.parent.GetComponent<Canvas>();
        selectMenuFab = Resources.Load<GameObject>("UI/SelectMenu");
        UpdateMode();
    }

    public void OnClick()
    {
        curMode++;
        if (!Enum.IsDefined(typeof(PointerMode), curMode))
        {
            curMode = 0;
        }
        UpdateMode();
    }

    public void UpdateMode() 
    {
        switch (curMode) 
        {
            //TODO show mode on button
            case PointerMode.Select:
                PlaceMenu.CloseCurMenu();
                ObjectPickup.pickupEnabled = false;
                StaticPlayerInput.PInput.currentActionMap["Click"].started += SelectClick;
                break;
            case PointerMode.Move:
                PlaceMenu.CloseCurMenu();
                SelectMenu.curSelected = null;
                ObjectPickup.pickupEnabled = true;
                try
                {
                    StaticPlayerInput.PInput.currentActionMap["Click"].started -= SelectClick;
                }
                catch { }
                break;
            case PointerMode.Place:
                PlaceMenu.CloseCurMenu();
                Instantiate(Resources.Load<GameObject>("UI/PlaceMenu"), myCanvas.transform);
                SelectMenu.curSelected = null;
                ObjectPickup.pickupEnabled = false;
                try
                {
                    StaticPlayerInput.PInput.currentActionMap["Click"].started -= SelectClick;
                }
                catch { }
                //TODO create place menu
                break;
        }
    }

    private void SelectClick(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!EventSystem.current.IsPointerOverGameObject()) 
        {
            SelectMenu.CloseCurMenu();
            if (PickingRaycast.TargetedObject)
            {
                Instantiate(selectMenuFab, myCanvas.transform);
                //need to add selection redirecting
                SelectMenu.curSelected = PickingRaycast.TargetedObject;
            }
        }
    }
}
