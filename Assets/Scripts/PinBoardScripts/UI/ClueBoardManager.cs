using UnityEngine;
using System.Collections.Generic;

public class ClueBoardManager : MonoBehaviour
{
    public static ClueBoardManager Instance;

    [Header("References")]
    public Transform dropZone;
    public Transform linesContainer;
    public CluePanelController cluePanel;

    [Header("Prefabs")]
    public GameObject clueCardPrefab;
    public GameObject linePrefab;

    [HideInInspector] public LevelData currentLevel;

    private readonly List<ClueCard> activeCards = new();
    private readonly List<ConnectionData> activeConnections = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void LoadLevel(LevelData level)
    {
        currentLevel = level;
        ClearBoard();
        cluePanel.Populate(level.clues);
    }

    public void RegisterConnection(ClueConnection pointA, ClueConnection pointB)
    {
        if (pointA == null || pointB == null) return;
        var cardA = pointA.parentCard;
        var cardB = pointB.parentCard;
        if (cardA == null || cardB == null) return;

        var newConnection = new ConnectionData(cardA.data.clueID, cardB.data.clueID);
        activeConnections.Add(newConnection);

        if (IsCorrectConnection(newConnection))
        {
            Debug.Log($"✅ Правильная связь: {cardA.data.clueName} ↔ {cardB.data.clueName}");
        }
        else
        {
            Debug.Log($"❌ Неправильная связь: {cardA.data.clueName} ↔ {cardB.data.clueName}");
        }

        CheckWinCondition();
    }

    private bool IsCorrectConnection(ConnectionData testConnection)
    {
        foreach (var conn in currentLevel.correctConnections)
        {
            bool matchAB = conn.clueA_ID == testConnection.clueA_ID && conn.clueB_ID == testConnection.clueB_ID;
            bool matchBA = conn.clueA_ID == testConnection.clueB_ID && conn.clueB_ID == testConnection.clueA_ID;
            if (matchAB || matchBA) return true;
        }
        return false;
    }

    private void CheckWinCondition()
    {
        int correctCount = 0;
        foreach (var conn in currentLevel.correctConnections)
        {
            foreach (var active in activeConnections)
            {
                bool matchAB = conn.clueA_ID == active.clueA_ID && conn.clueB_ID == active.clueB_ID;
                bool matchBA = conn.clueA_ID == active.clueB_ID && conn.clueB_ID == active.clueA_ID;
                if (matchAB || matchBA)
                {
                    correctCount++;
                    break;
                }
            }
        }

        if (correctCount >= currentLevel.correctConnections.Length)
        {
            Debug.Log("🎉 Все связи найдены! Уровень пройден!");
        }
    }

    public void ClearBoard()
    {
        foreach (var card in activeCards)
        {
            if (card != null)
                Destroy(card.gameObject);
        }
        activeCards.Clear();

        foreach (Transform line in linesContainer)
        {
            Destroy(line.gameObject);
        }

        activeConnections.Clear();
    }
}
