using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    void Update()
    {
        DragCheck();
    }
    protected void DragCheck()
    {
        if (isDragging)
        {
            // Follow mouse position
            Vector3 mousePosition = GetMouseWorldPosition(8);
            transform.position = mousePosition + offset;
        }
    }

    void OnMouseDown()
    {
        // Calculate offset between object position and mouse position
        offset = transform.position - GetMouseWorldPosition(8);
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition(int border)
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.x = Mathf.Max(mousePoint.x, border);
        mousePoint.x = Mathf.Min(mousePoint.x, Screen.width-border);
        mousePoint.y = Mathf.Max(mousePoint.y, border);
        mousePoint.y = Mathf.Min(mousePoint.y, Screen.height-border);
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
