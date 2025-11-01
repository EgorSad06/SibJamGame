using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Transform originalParent;

    [Header("Ќастройки области перетаскивани€")]
    [Tooltip("ќбласть, внутри которой можно перемещать объект (обычно BoardArea)")]
    public RectTransform restrictToArea;

    [Tooltip("≈сли включено Ч карточка возвращаетс€ на место, если отпущена вне области")]
    public bool returnIfOutside = true;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // ищем Canvas выше по иерархии
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
            Debug.LogError("[DraggableUI] Canvas not found in parent objects!");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // сохран€ем начальные параметры
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;

        // поднимаем на верхний уровень в Canvas, чтобы не перекрывало другие UI
        transform.SetParent(canvas.transform, true);

        // визуально делаем прозрачнее
        canvasGroup.alpha = 0.8f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) return;

        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out localPoint))
        {
            rectTransform.anchoredPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (restrictToArea == null)
        {
            // если область не задана Ч просто остаЄтс€ на месте
            transform.SetParent(canvas.transform, true);
            return;
        }

        if (RectTransformUtility.RectangleContainsScreenPoint(restrictToArea, eventData.position, canvas.worldCamera))
        {
            // если отпущено в области Ч делаем BoardArea родителем
            transform.SetParent(restrictToArea, true);
        }
        else if (returnIfOutside)
        {
            // если отпущено вне и нужно вернуть Ч возвращаем обратно
            transform.SetParent(originalParent, true);
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
