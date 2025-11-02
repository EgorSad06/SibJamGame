using UnityEngine;
using System.Collections.Generic;

public class ClueBoardManager : MonoBehaviour
{
    public static ClueBoardManager Instance;

    [Header("References")]
    public RectTransform boardArea;
    public Transform linesContainer;

    [Header("Prefabs")]
    public GameObject clueCardPrefab;
    public GameObject linePrefab; // prefab с UILineRenderer

    [Header("Level")]
    public LevelData currentLevel;

    private List<ClueCard> spawned = new List<ClueCard>();
    private ClueCard selectedCard;

    private readonly List<ConnectionData> activeConnections = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (currentLevel != null) LoadLevel(currentLevel);
    }

    public void LoadLevel(LevelData level)
    {
        currentLevel = level;
        ClearBoard();

        foreach (var c in level.clues)
        {
            var go = Instantiate(clueCardPrefab, boardArea);
            var card = go.GetComponent<ClueCard>();
            card.Initialize(c);

            // random position inside boardArea
            var rt = go.GetComponent<RectTransform>();
            float margin = 80f;
            float x = Random.Range(-boardArea.rect.width / 2 + margin, boardArea.rect.width / 2 - margin);
            float y = Random.Range(-boardArea.rect.height / 2 + margin, boardArea.rect.height / 2 - margin);
            rt.anchoredPosition = new Vector2(x, y);

            spawned.Add(card);
        }
    }

    public void ClearBoard()
    {
        foreach (Transform t in boardArea) Destroy(t.gameObject);
        spawned.Clear();
        selectedCard = null;

        if (linesContainer != null)
        {
            foreach (Transform t in linesContainer) Destroy(t.gameObject);
        }
        activeConnections.Clear();
    }

    public void OnCardClicked(ClueCard card)
    {
        if (card == null) return;

        // если кликнули на уже выбранную — снимаем выбор
        if (selectedCard == card)
        {
            selectedCard.SetSelected(false);
            selectedCard = null;
            return;
        }

        // если ранее была выбрана первая карточка — соединяем
        if (selectedCard == null)
        {
            selectedCard = card;
            selectedCard.SetSelected(true);
            return;
        }

        // соединяем selectedCard и card (если разные)
        if (selectedCard != null && card != null && selectedCard != card)
        {
            CreateLineBetween(selectedCard, card);
            RegisterConnection(selectedCard, card);

            // снимем выделение первой и сбросим выбор
            selectedCard.SetSelected(false);
            selectedCard = null;
        }
    }

    private void CreateLineBetween(ClueCard a, ClueCard b)
    {
        if (linePrefab == null)
        {
            Debug.LogWarning("Line prefab not set");
            return;
        }

        GameObject lineObj = Instantiate(linePrefab, linesContainer != null ? linesContainer : boardArea);
        var lr = lineObj.GetComponent<UILineRenderer>();
        if (lr == null)
        {
            Debug.LogWarning("Line prefab missing UILineRenderer");
            Destroy(lineObj);
            return;
        }

        lr.pointA = a.GetComponent<RectTransform>();
        lr.pointB = b.GetComponent<RectTransform>();
        // color and thickness can be set in prefab
    }

    // раньше: private void RegisterConnection(ClueCard a, ClueCard b)
    public void RegisterConnection(ClueCard a, ClueCard b)
    {
        if (a == null || b == null) return;

        var conn = new ConnectionData(a.data.clueID, b.data.clueID);
        activeConnections.Add(conn);

        bool correct = false;
        if (currentLevel != null && currentLevel.correctConnections != null)
        {
            foreach (var cc in currentLevel.correctConnections)
            {
                bool match = (cc.clueA_ID == conn.clueA_ID && cc.clueB_ID == conn.clueB_ID) ||
                             (cc.clueA_ID == conn.clueB_ID && cc.clueB_ID == conn.clueA_ID);
                if (match) { correct = true; break; }
            }
        }
        Debug.Log(correct ? $"✅ Correct: {a.data.clueName} ↔ {b.data.clueName}" : $"❌ Wrong: {a.data.clueName} ↔ {b.data.clueName}");
        CheckWinCondition();
    }


    private void CheckWinCondition()
    {
        if (currentLevel == null || currentLevel.correctConnections == null) return;
        int found = 0;
        foreach (var need in currentLevel.correctConnections)
        {
            foreach (var a in activeConnections)
            {
                bool match = (need.clueA_ID == a.clueA_ID && need.clueB_ID == a.clueB_ID) ||
                             (need.clueA_ID == a.clueB_ID && need.clueB_ID == a.clueA_ID);
                if (match) { found++; break; }
            }
        }
        if (found >= currentLevel.correctConnections.Length)
        {
            Debug.Log("🎉 Level complete!");
            // call win UI...
        }
    }
}
