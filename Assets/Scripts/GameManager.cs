using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public LevelData currentLevel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        ClueBoardManager.Instance.LoadLevel(currentLevel);
    }
}
