using UnityEngine;
using TMPro;

public class MainSceneScoreUI : MonoBehaviour
{
    public TMP_Text bestScoreText;

    void Start()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = $"BestScore : {bestScore}";
    }
}
