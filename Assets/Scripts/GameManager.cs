using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ClueBoardManager clueBoardManager;
    public LevelData currentLevel;

    private void Start()
    {
        if (clueBoardManager && currentLevel != null)
            clueBoardManager.LoadLevel(currentLevel);
    }
}
