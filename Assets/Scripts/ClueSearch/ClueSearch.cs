using Unity.Cinemachine;
using UnityEngine;

public class ClueSearch : MonoBehaviour
{
    [SerializeField] public GameObject Content;
    [SerializeField] public GameObject Player;

    public CinemachineCamera MinigameCinemachine;

    private bool isMiniGameActive = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Content.SetActive(false);
        MinigameCinemachine = GetComponent<CinemachineCamera>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isMiniGameActive)
            {
                EndMiniGame();
            }
            else
            {
                StartMiniGame();
            }
        }
    }

    void StartMiniGame()
    {
        isMiniGameActive = true;
        Content.SetActive(true);
        Player.SetActive(false);

        MinigameCinemachine.enabled = true;
        MinigameCinemachine.Prioritize();
    }
    void EndMiniGame()
    {
        isMiniGameActive = false;
        Content.SetActive(false);
        Player.SetActive(true);

        MinigameCinemachine.enabled = false;
    }

}
