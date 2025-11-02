using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsScriptableObjectScript", menuName = "Scriptable Objects/ItemsScriptableObject")]
public class ItemsScriptableObjectScript : ScriptableObject
{
    [SerializeField] public GameObject[] Items;
    [SerializeField] public GameObject[] SpecialItems;
    [SerializeField] public GameObject[] ToolItem;
}
