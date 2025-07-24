using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("게임 시작 UI")]
    public GameObject gameStartUI;

    [Header("게임 중 점수 UI")]
    public TMP_Text scoreText;

    [Header("게임 오버 패널 및 텍스트")]
    public GameObject gameOverUI;
    public TMP_Text currentScoreText;
    public TMP_Text bestScoreText;

    private int score = 0;
    private int bestScore = 0;

    // ✅ 씬별로 페이드 인 여부 추적
    private static HashSet<string> fadedScenes = new HashSet<string>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        StartCoroutine(InitialSetup());
    }

    private IEnumerator InitialSetup()
    {
        Time.timeScale = 0f;
        gameStartUI?.SetActive(true);
        UpdateScoreUI();
        gameOverUI?.SetActive(false);

        string currentScene = SceneManager.GetActiveScene().name;

        // ✅ 이 씬이 처음 로드된 경우에만 페이드 인
        if (!fadedScenes.Contains(currentScene))
        {
            fadedScenes.Add(currentScene);
            yield return SceneFader.Instance.FadeInOnly();
        }
    }

    public void OnClickStartGame()
    {
        Time.timeScale = 1f;
        gameStartUI?.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"SCORE : {score}";
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        if (currentScoreText != null)
            currentScoreText.text = $"SCORE : {score}";

        if (bestScoreText != null)
            bestScoreText.text = $"BEST : {bestScore}";

        gameOverUI?.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneFader.Instance.FadeAndLoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMain()
    {
        Time.timeScale = 1f;
        SceneFader.Instance.FadeAndLoadScene("MainScene");
    }
}