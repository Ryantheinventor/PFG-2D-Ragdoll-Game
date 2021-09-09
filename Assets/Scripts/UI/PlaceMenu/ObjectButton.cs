using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectButton : MonoBehaviour
{
    public SpawnableObject myObject;
    void Start()
    {
        transform.Find("Icon").GetComponent<Image>().sprite = myObject.menuIcon;
    }

    public void OnClick() 
    {
        Instantiate(myObject.prefab);
    }

}
