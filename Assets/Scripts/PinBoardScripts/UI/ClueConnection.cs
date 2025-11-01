using UnityEngine;
using UnityEngine.EventSystems;

public class ClueConnection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public ClueCard parentCard;
    private LineRenderer line;
    private bool isDrawing;

    public void OnPointerDown(PointerEventData eventData)
    {
        var lineObj = Instantiate(ClueBoardManager.Instance.linePrefab, ClueBoardManager.Instance.linesContainer);
        line = lineObj.GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.02f;
        line.endWidth = 0.02f;
        line.SetPosition(0, transform.position);
        isDrawing = true;
    }

    void Update()
    {
        if (isDrawing && line != null)
            line.SetPosition(1, Input.mousePosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDrawing = false;
        var hit = eventData.pointerCurrentRaycast.gameObject;
        if (hit != null && hit.TryGetComponent(out ClueConnection target) && target != this)
        {
            line.SetPosition(1, target.transform.position);
            ClueBoardManager.Instance.RegisterConnection(this, target);
        }
        else
        {
            Destroy(line.gameObject);
        }
    }
}
