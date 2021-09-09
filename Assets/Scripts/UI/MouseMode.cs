using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MouseMode : MonoBehaviour
{
    public PointerMode curMode = PointerMode.Move;

    public enum PointerMode 
    {
        Select,
        Move,
        Place
    }

    private void Start()
    {
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
            case PointerMode.Select:
                ObjectPickup.pickupEnabled = false;
                break;
            case PointerMode.Move:
                ObjectPickup.pickupEnabled = true;
                break;
        }
    }
}
