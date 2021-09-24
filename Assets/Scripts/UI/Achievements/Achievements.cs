using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Achievements : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public int totalAchievementCount = 1;
    struct AchievementData
    {
        public string name;
        public string desc;
    }

    //static
    private static List<string> achieved = new List<string>();
    private static Transform canvas;
    private static GameObject prefab;
    private static GameObject activeAchievementBox = null;
    private static Queue<AchievementData> achievementQueue = new Queue<AchievementData>();
    public static void NewAchievement(string achievementName, string achievementDesc) 
    {
        
        if (!canvas)
        {
            canvas = FindObjectOfType<Canvas>().transform;
        }
        if (!prefab) 
        {
            prefab = Resources.Load<GameObject>("UI/AchievementBox");
        }

        if (!achieved.Contains(achievementName)) 
        {
            Debug.Log(achievementName);
            achieved.Add(achievementName);
            AchievementData newData = new AchievementData() { name = achievementName, desc = achievementDesc };
            if (activeAchievementBox)
            {
                achievementQueue.Enqueue(newData);
            }
            else 
            {
                ShowAchievement(newData);
            }
            
        }
    }

    private static void ShowAchievement(AchievementData aData) 
    {
        GameObject newBox = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation) as GameObject;
        newBox.transform.SetParent(canvas, false);
        newBox.GetComponent<Achievements>().text1.text = "Achievement Get: " + aData.name;
        newBox.GetComponent<Achievements>().text2.text = aData.desc;
        activeAchievementBox = newBox;
    }

    private void OnDestroy()
    {
        if (achievementQueue.Count > 0)
        {
            ShowAchievement(achievementQueue.Dequeue());
        }
        if (achieved.Count >= totalAchievementCount - 1) 
        {
            NewAchievement("All The Things", "Good job you got all the achievements... I still hate you... I want my box back.");
        }
    }

}

