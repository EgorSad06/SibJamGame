using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DebugLine : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var pointer = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, results);

            Debug.Log("[UIRaycastDebugger] Hits:");
            foreach (var hit in results)
                Debug.Log("  " + hit.gameObject.name);
        }
    }
}
