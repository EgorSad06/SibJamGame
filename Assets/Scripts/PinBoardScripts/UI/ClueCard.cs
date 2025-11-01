using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClueCard : MonoBehaviour
{
    public Image photoImage;
    public TMP_Text titleText;
    public ClueConnection connectionPoint;

    [HideInInspector] public ClueData data;

    public void Initialize(ClueData clue)
    {
        data = clue;
        photoImage.sprite = clue.clueImage;
        titleText.text = clue.clueName;
        if (connectionPoint != null)
            connectionPoint.parentCard = this;
    }
}
