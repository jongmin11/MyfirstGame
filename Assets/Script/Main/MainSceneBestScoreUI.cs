using UnityEngine;
using TMPro;
using System;

[Serializable]
public struct SceneBestEntry
{
    [Tooltip("빌드 세팅에 등록된 씬 이름과 정확히 일치해야 합니다")]
    public string sceneName;
    public TextMeshProUGUI bestScoreText;
}

public class MainSceneBestScoreUI : MonoBehaviour
{
    [Header("씬별 최고점수 매핑")]
    [SerializeField] private SceneBestEntry[] entries;

    private void Start()
    {
        foreach (var e in entries)
        {
            if (e.bestScoreText == null)
            {
                Debug.LogError($"[{e.sceneName}] TextMeshProUGUI가 할당되지 않았습니다.", this);
                continue;
            }

            // "MyGameScene_BestScore" 형태의 키로 불러오기
            string key = $"{e.sceneName}_BestScore";
            int best = PlayerPrefs.GetInt(key, 0);
            e.bestScoreText.text = $"{e.sceneName} 최고점수: {best}";
        }
    }
}