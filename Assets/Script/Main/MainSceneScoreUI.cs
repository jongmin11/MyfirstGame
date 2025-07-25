using UnityEngine;
using TMPro;

public class MainSceneScoreUI : MonoBehaviour
{
    [Header("UI 연결")]
    [SerializeField] private TextMeshProUGUI scoreText;  // Inspector에 드래그 앤 드롭

    private const string BestScoreKey = "BestScore";

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("MainSceneScoreUI: scoreText가 할당되지 않았습니다!", this);
            return;
        }

        // 저장된 최고점수 불러오기
        int bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        Debug.Log($"불러온 최고점수: {bestScore}");

        // 텍스트에 표시
        scoreText.text = $"최고점수: {bestScore}";
    }
}