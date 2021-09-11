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
    private bool selectEnabled = false;
    public enum PointerMode 
    {
        Select,
        Move,
        //Place,
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
        StaticPlayerInput.PInput.currentActionMap["Key3"].started += SwitchToDoll;
        //StaticPlayerInput.PInput.currentActionMap["Key3"].started += SwitchToPlace;
        //StaticPlayerInput.PInput.currentActionMap["Key4"].started += SwitchToDoll;
        UpdateMode();
    }

    private void SwitchToSelect(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Achievements.NewAchievement("I did a thing", "Swaping cursor modes is what you did.");
        curMode = PointerMode.Select;
        UpdateMode();
    }
    private void SwitchToMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Achievements.NewAchievement("I did a thing", "Swaping cursor modes is what you did.");
        curMode = PointerMode.Move;
        UpdateMode();
    }
    //private void SwitchToPlace(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    //{
    //    Achievements.NewAchievement("I did a thing", "Swaping cursor modes is what you did.");
    //    curMode = PointerMode.Place;
    //    UpdateMode();
    //}
    private void SwitchToDoll(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Achievements.NewAchievement("I did a thing", "Swaping cursor modes is what you did.");
        curMode = PointerMode.PlayerDoll;
        UpdateMode();
    }

    public void OnClick()
    {
        Achievements.NewAchievement("I did a thing", "Swaping cursor modes is what you did.");
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
                if (!selectEnabled) 
                {
                    StaticPlayerInput.PInput.currentActionMap["Click"].started += SelectClick;
                    selectEnabled = true;
                }
                PlaceMenu.OpenMenu();
                ObjectPickup.pickupEnabled = false;
                PlayerArms.DisablePlayerArms();
                break;
            case PointerMode.Move:
                selectEnabled = false;
                PlaceMenu.OpenMenu();
                SelectMenu.curSelected = null;
                ObjectPickup.pickupEnabled = true;
                PlayerArms.DisablePlayerArms();
                StaticPlayerInput.PInput.currentActionMap["Click"].started -= SelectClick;
                break;
            //case PointerMode.Place:
            //    PlaceMenu.OpenMenu();
            //    SelectMenu.curSelected = null;
            //    ObjectPickup.pickupEnabled = false;
            //    PlayerArms.DisablePlayerArms();
            //    StaticPlayerInput.PInput.currentActionMap["Click"].started -= SelectClick;
            //    break;
            case PointerMode.PlayerDoll:
                selectEnabled = false;
                PlaceMenu.CloseCurMenu();
                SelectMenu.curSelected = null;
                ObjectPickup.pickupEnabled = false;
                PlayerArms.ActivatePlayerArms();
                StaticPlayerInput.PInput.currentActionMap["Click"].started -= SelectClick;
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
                (Instantiate(selectMenuFab, selectMenuFab.transform.position, selectMenuFab.transform.rotation) as GameObject).transform.SetParent(myCanvas.transform, false);
                //need to add selection redirecting
                SelectMenu.curSelected = PickingRaycast.TargetedObject;
            }
        }
    }
}
