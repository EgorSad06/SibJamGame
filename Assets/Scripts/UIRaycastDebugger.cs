using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIRaycastDebugger : MonoBehaviour
{
    public Canvas canvas;
    private UnityEngine.UI.GraphicRaycaster gr;
    private PointerEventData pointerData;
    private EventSystem es;

    void Start()
    {
        if (canvas == null) canvas = FindObjectOfType<Canvas>();
        if (canvas != null) gr = canvas.GetComponent<UnityEngine.UI.GraphicRaycaster>();
        es = EventSystem.current;
        pointerData = new PointerEventData(es);
    }

    void Update()
    {
        if (gr == null || es == null) return;

        pointerData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerData, results);

        if (results.Count > 0)
        {
            string s = "[UIRaycastDebugger] Top hits:";
            for (int i = 0; i < Mathf.Min(5, results.Count); i++)
                s += $"\n  {i}: {results[i].gameObject.name} (module: {results[i].module})";
            Debug.Log(s);
        }
    }
}
