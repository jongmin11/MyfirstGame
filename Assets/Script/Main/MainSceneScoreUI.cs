using UnityEngine;
using TMPro;

public class MainSceneScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;

    void Start()
    {

        int bestScore = PlayerPrefs.GetInt("SceneStack_BestScore", 0);

        if (scoreText != null)
            scoreText.text = $"Game 최고점수: {bestScore}";
    }
}