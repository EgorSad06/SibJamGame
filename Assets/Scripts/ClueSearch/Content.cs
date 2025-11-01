using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Content : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public static List<GameObject> AllItems;
    static Content()
    {
        AllItems.Add(Resources.Load<GameObject>("Assets/Prefabs/ClueSearch/Glasses"));
        AllItems.Add(Resources.Load<GameObject>("Assets/Prefabs/ClueSearch/Paper"));
        AllItems.Add(Resources.Load<GameObject>("Assets/Prefabs/ClueSearch/Pen"));
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int n = Random.Range(4, 8);
        for (int i = 0; i < n; i++)
        {
            GameObject newObj = Instantiate(
                AllItems[Random.Range(0,AllItems.Count)],
                new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-1.5f, 1.5f)),
                new Quaternion(0,0,Random.Range(0,360),0),
                transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
