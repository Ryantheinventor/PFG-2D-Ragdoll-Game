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
    public Image modeIconImage;
    private Sprite[] modeIcons;
    public static MouseMode instance = null;
    public enum PointerMode 
    {
        Select,
        Move,
        Place,
        PlayerDoll
    }

    private void Start()
    {
        instance = this;
        modeIconImage = transform.Find("MouseIconImage").GetComponent<Image>();
        modeIcons = Resources.LoadAll<Sprite>("UI/MouseModeIcons");
        myCanvas = transform.parent.GetComponent<Canvas>();
        selectMenuFab = Resources.Load<GameObject>("UI/SelectMenu");
        StaticPlayerInput.PInput.currentActionMap["Key1"].started += SwitchToSelect;
        StaticPlayerInput.PInput.currentActionMap["Key2"].started += SwitchToMove;
        StaticPlayerInput.PInput.currentActionMap["Key3"].started += SwitchToPlace;
        StaticPlayerInput.PInput.currentActionMap["Key4"].started += SwitchToDoll;
        UpdateMode();
    }

    private void SwitchToSelect(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        curMode = PointerMode.Select;
        UpdateMode();
    }
    private void SwitchToMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        curMode = PointerMode.Move;
        UpdateMode();
    }
    private void SwitchToPlace(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        curMode = PointerMode.Place;
        UpdateMode();
    }
    private void SwitchToDoll(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        curMode = PointerMode.PlayerDoll;
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
                PlayerArms.DisablePlayerArms();
                break;

            case PointerMode.Move:
                PlaceMenu.CloseCurMenu();
                SelectMenu.curSelected = null;
                ObjectPickup.pickupEnabled = true;
                PlayerArms.DisablePlayerArms();
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
                PlayerArms.DisablePlayerArms();
                try
                {
                    StaticPlayerInput.PInput.currentActionMap["Click"].started -= SelectClick;
                }
                catch { }
                break;
            case PointerMode.PlayerDoll:
                PlaceMenu.CloseCurMenu();
                SelectMenu.curSelected = null;
                ObjectPickup.pickupEnabled = false;
                PlayerArms.ActivatePlayerArms();
                try
                {
                    StaticPlayerInput.PInput.currentActionMap["Click"].started -= SelectClick;
                }
                catch { }
                break;
        }
        modeIconImage.sprite = modeIcons[(int)curMode];
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
