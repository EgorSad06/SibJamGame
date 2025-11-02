using UnityEngine;
using UnityEngine.EventSystems;

public class ClueConnection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public ClueCard parentCard;
    private UILineRenderer currentLine;

    public void AssignDefaults()
    {
        // Called from ClueCard.Initialize — лёгкая проверка на null
        if (parentCard == null)
        {
            parentCard = GetComponentInParent<ClueCard>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"[ClueConnection] OnPointerDown on {name}, parentCard={(parentCard != null ? parentCard.name : "null")}");
        if (ClueBoardManager.Instance == null)
        {
            Debug.LogError("[ClueConnection] ClueBoardManager.Instance == null");
            return;
        }
        if (ClueBoardManager.Instance.linePrefab == null)
        {
            Debug.LogError("[ClueConnection] linePrefab not assigned in ClueBoardManager");
            return;
        }

        GameObject lineObj = Instantiate(ClueBoardManager.Instance.linePrefab, ClueBoardManager.Instance.linesContainer ? ClueBoardManager.Instance.linesContainer : ClueBoardManager.Instance.boardArea);
        currentLine = lineObj.GetComponent<UILineRenderer>();
        if (currentLine == null)
        {
            Debug.LogError("[ClueConnection] linePrefab has no UILineRenderer");
            Destroy(lineObj);
            return;
        }

        currentLine.pointA = transform as RectTransform;
        currentLine.pointB = transform as RectTransform; // стартовая точка
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (currentLine == null) return;

        if (eventData.pointerEnter != null)
        {
            var target = eventData.pointerEnter.GetComponentInParent<ClueConnection>();
            if (target != null && target != this)
            {
                currentLine.pointB = target.transform as RectTransform;
                // Регистрируем соединение между карточками (не между точками)
                if (this.parentCard != null && target.parentCard != null)
                {
                    ClueBoardManager.Instance.RegisterConnection(this.parentCard, target.parentCard);
                }
                else
                {
                    Debug.LogWarning("[ClueConnection] parentCard is null for one of points");
                }
                currentLine = null;
                return;
            }
        }

        Destroy(currentLine.gameObject);
        currentLine = null;
    }

}
