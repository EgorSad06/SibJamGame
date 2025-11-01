using UnityEngine;

public class CluePanelController : MonoBehaviour
{
    public GameObject clueCardPrefab;

    public void Populate(ClueData[] clues)
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        foreach (var clue in clues)
        {
            var cardObj = Instantiate(clueCardPrefab, transform);
            var card = cardObj.GetComponent<ClueCard>();
            card.Initialize(clue);
        }
    }
}
