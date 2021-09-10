using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLoader : MonoBehaviour
{
    public string prefabPath = "";
    public GameObject Prefab 
    {
        get => Resources.Load<GameObject>(prefabPath);
    }
}
