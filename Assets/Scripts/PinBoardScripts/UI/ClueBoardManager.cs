using UnityEngine;
using System.Collections.Generic;

public class ClueBoardManager : MonoBehaviour
{
    public static ClueBoardManager Instance;

    [Header("References")]
    public Transform boardArea;
    public Transform linesContainer; // контейнер для линий (опционально)

    [Header("Prefabs")]
    public GameObject clueCardPrefab;
    public GameObject linePrefab; // <- добавленное поле

    [HideInInspector] public LevelData currentLevel;

    private List<ClueCard> spawnedClues = new List<ClueCard>();
    private List<ConnectionData> activeConnections = new List<ConnectionData>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    public void LoadLevel(LevelData level)
    {
        currentLevel = level;
        ClearBoard();

        if (level == null || level.clues == null) return;

        foreach (var clue in level.clues)
        {
            var go = Instantiate(clueCardPrefab, boardArea);
            var card = go.GetComponent<ClueCard>();
            if (card != null)
            {
                card.Initialize(clue);
                spawnedClues.Add(card);
            }
            else
            {
                Debug.LogError("Prefab ClueCard не содержит компонент ClueCard.");
            }
        }
    }

    public GameObject CreateLine()
    {
        if (linePrefab == null)
        {
            Debug.LogWarning("Line Prefab не назначен в ClueBoardManager.");
            return null;
        }

        Transform parent = linesContainer != null ? linesContainer : boardArea;
        var lineObj = Instantiate(linePrefab, parent);
        return lineObj;
    }

    public void RegisterConnection(ClueConnection a, ClueConnection b)
    {
        if (a == null || b == null) return;
        if (a.parentCard == null || b.parentCard == null) return;

        var conn = new ConnectionData(a.parentCard.data.clueID, b.parentCard.data.clueID);
        activeConnections.Add(conn);

        bool correct = false;
        if (currentLevel != null && currentLevel.correctConnections != null)
        {
            foreach (var cc in currentLevel.correctConnections)
            {
                bool match = (cc.clueA_ID == conn.clueA_ID && cc.clueB_ID == conn.clueB_ID)
                             || (cc.clueA_ID == conn.clueB_ID && cc.clueB_ID == conn.clueA_ID);
                if (match) { correct = true; break; }
            }
        }

        Debug.Log(correct ? "✅ Правильная связь" : "❌ Неправильная связь");
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
                bool match = (need.clueA_ID == a.clueA_ID && need.clueB_ID == a.clueB_ID)
                             || (need.clueA_ID == a.clueB_ID && need.clueB_ID == a.clueA_ID);
                if (match) { found++; break; }
            }
        }

        if (found >= currentLevel.correctConnections.Length)
        {
            Debug.Log("🎉 Уровень пройден!");
            // здесь куда-нибудь событие победы
        }
    }

    public void ClearBoard()
    {
        foreach (var c in spawnedClues)
            if (c != null) Destroy(c.gameObject);
        spawnedClues.Clear();

        if (linesContainer != null)
        {
            foreach (Transform t in linesContainer)
                Destroy(t.gameObject);
        }
        else
        {
            // если linesContainer не задан — удаляем дочерние объекты, которые выглядят как линии
            if (boardArea != null)
            {
                for (int i = boardArea.childCount - 1; i >= 0; i--)
                {
                    var child = boardArea.GetChild(i);
                    if (child.name.ToLower().Contains("line") || child.GetComponent<LineRenderer>() != null)
                        Destroy(child.gameObject);
                }
            }
        }

        activeConnections.Clear();
    }
}
