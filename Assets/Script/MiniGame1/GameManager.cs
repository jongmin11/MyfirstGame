using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject gameStartUI;  // 시작 패널
    public GameObject gameOverUI;   // 게임오버 패널

    public TMP_Text nowScoreText;   // 현재 점수 텍스트 (게임오버에서만 표시)
    public TMP_Text bestScoreText;  // 최고 점수 텍스트 (게임오버에서만 표시)

    private int score = 0;
    private int bestScore = 0;

    private const string FIRST_PLAY_KEY = "HasPlayedBefore";

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // 처음 실행이면 시작 UI 띄우고 멈춤
        if (!PlayerPrefs.HasKey(FIRST_PLAY_KEY))
        {
            Time.timeScale = 0f;
            gameStartUI.SetActive(true);
            PlayerPrefs.SetInt(FIRST_PLAY_KEY, 1);
        }
        else
        {
            Time.timeScale = 1f;
            gameStartUI.SetActive(false);
        }

        gameOverUI.SetActive(false);
    }

    public void OnClickStartGame()
    {
        Time.timeScale = 1f;
        gameStartUI.SetActive(false);
    }

    public void AddScore(int value)
    {
        score += value;
        // 스코어는 이미지 방식으로 표현하므로 텍스트는 건드리지 않음
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        // 텍스트에 현재/최고 점수 반영
        if (nowScoreText != null)
            nowScoreText.text = score.ToString();

        if (bestScoreText != null)
            bestScoreText.text = bestScore.ToString();

        gameOverUI.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }
}