using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClueCard : MonoBehaviour
{
    public Image photoImage;
    public TMP_Text titleText;
    public ClueConnection connectionPoint;
    public CanvasGroup canvasGroup;

    [HideInInspector] public ClueData data;

    private void Awake()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        if (photoImage == null) photoImage = GetComponentInChildren<Image>();
        if (titleText == null) titleText = GetComponentInChildren<TMP_Text>();
    }

    public void Initialize(ClueData clue)
    {
        data = clue;
        if (titleText != null) titleText.text = clue != null ? clue.clueName : "";
        if (photoImage != null && clue != null) photoImage.sprite = clue.clueImage;
        if (connectionPoint != null) connectionPoint.parentCard = this;
        Debug.Log($"[ClueCard] Initialized {name} with {clue?.clueName}");
    }
}
