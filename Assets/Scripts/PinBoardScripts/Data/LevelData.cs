using UnityEngine;

[CreateAssetMenu(menuName = "Detective/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public ClueData[] clues;
    public ConnectionData[] correctConnections;
}
