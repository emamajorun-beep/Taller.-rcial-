using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    
    private TMP_Text coinText;
    private TMP_Text poisonText;
    private TMP_Text totalText;
    private TMP_Text livesText;

    
    private float globalTime;
    public int scoreCoin;
    public int scorePoison;
    public int playerLives;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        
        if (playerLives <= 0)
        {
            playerLives = 4;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameOver")
        {
            Time.timeScale = 0f;
        }
        else
        {
            coinText = GameObject.Find("MonedasCant")?.GetComponent<TextMeshProUGUI>();
            poisonText = GameObject.Find("PocionCant")?.GetComponent<TextMeshProUGUI>();
            totalText = GameObject.Find("PuntosTCant")?.GetComponent<TextMeshProUGUI>();
            livesText = GameObject.Find("Vida")?.GetComponent<TextMeshProUGUI>();

            UpdateScoreUI();
        }
    }

    public void TotalTime(float timeScene)
    {
        globalTime += timeScene;
    }

    public void TotalCoin(int coin)
    {
        scoreCoin += coin;
        UpdateScoreUI();
    }

    public void TotalPoison(int poison)
    {
        scorePoison += poison;
        UpdateScoreUI();
    }

    public void AddLives(int lives)
    {
        playerLives += lives;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (coinText != null) coinText.text = scoreCoin.ToString();
        if (poisonText != null) poisonText.text = scorePoison.ToString();
        if (totalText != null) totalText.text = (scoreCoin + scorePoison).ToString();
        if (livesText != null) livesText.text = playerLives.ToString();
    }

    public float GlobalTime
    {
        get => globalTime;
        set => globalTime = value;
    }
}
