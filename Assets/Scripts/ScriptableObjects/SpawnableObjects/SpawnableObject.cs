using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpawnableObject", menuName = "Custom Objects/SpawnableObject")]
public class SpawnableObject : ScriptableObject
{
    public string objectName = "Name";
    public Sprite menuIcon;
    public GameObject prefab;
}
