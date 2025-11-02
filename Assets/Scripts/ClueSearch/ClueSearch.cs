using Unity.Cinemachine;
using UnityEngine;

public class ClueSearch : MonoBehaviour
{
    [SerializeField] public GameObject Content;
    [SerializeField] public GameObject Player;
    [SerializeField] public ItemsScriptableObjectScript ItemsScriptableObject;


    public CinemachineCamera MinigameCinemachine;

    private bool isMiniGameActive = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Content.SetActive(false);
        MinigameCinemachine = GetComponent<CinemachineCamera>();
        MinigameCinemachine.enabled = false;

        // Add specail target item
        Vector3 pos = new Vector3(Random.Range(-12f, 12f), Random.Range(-6f, 6f), -0.51f);
        GameObject target = Instantiate(
            ItemsScriptableObject.SpecialItems[Random.Range(0, ItemsScriptableObject.SpecialItems.Length)],
            pos,
            Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward),
            Content.transform);

        // Add items
        pos.z = -0.511f;
        int n = Random.Range(8, 14); // 6-12 items
        for (int i = 2; i < n; i++)
        {
            Instantiate(
                ItemsScriptableObject.Items[Random.Range(0, ItemsScriptableObject.Items.Length)],
                pos,
                Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward),
                Content.transform);
            pos = new Vector3(Random.Range(-12f, 12f), Random.Range(-6f, 6f), -0.51f - i * 0.001f);
        }

        // Add tool
        GameObject tool = Instantiate(
            ItemsScriptableObject.ToolItem[0],
            new Vector3(12f, -6f, -0.6f),
            new Quaternion(),
            Content.transform);
        tool.GetComponent<MagnifyingGlass>().TargetObject = target.gameObject.transform;
        tool.GetComponent<MagnifyingGlass>().TargetReached += (s, e) => EndMiniGame();
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
