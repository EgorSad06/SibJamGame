using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ClueCard : MonoBehaviour
{
    public Image photoImage;
    public TMP_Text titleText;
    public Image highlightImage; // полупрозрачная рамка/фон для выделения

    [HideInInspector] public ClueData data;
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnClicked);
        }
        if (highlightImage != null) highlightImage.gameObject.SetActive(false);
    }

    public void Initialize(ClueData clue)
    {
        data = clue;
        if (photoImage != null) photoImage.sprite = clue?.clueImage;
        if (titleText != null) titleText.text = clue?.clueName ?? "";
    }

    private void OnClicked()
    {
        ClueBoardManager.Instance.OnCardClicked(this);
    }

    public void SetSelected(bool selected)
    {
        if (highlightImage != null) highlightImage.gameObject.SetActive(selected);
    }

    private void OnDestroy()
    {
        if (btn != null) btn.onClick.RemoveListener(OnClicked);
    }
}
