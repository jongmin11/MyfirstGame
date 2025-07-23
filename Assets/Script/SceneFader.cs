using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;

    public Image fadeImage;      // 검정 이미지
    public GameObject loadingUI; // 로딩 전체 UI 패널
    public Slider loadingBar;    // 슬라이더

    public float fadeDuration = 1f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // ✅ UI도 같이 유지
        if (fadeImage != null)
            DontDestroyOnLoad(fadeImage.transform.root.gameObject);
        if (loadingUI != null)
            DontDestroyOnLoad(loadingUI.transform.root.gameObject);
    }

    public void FadeAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeThenLoad(sceneName));
    }

    private IEnumerator FadeThenLoad(string sceneName)
    {
        Debug.Log("🔥 씬 전환 시작");

        // ✅ 무조건 로딩UI 활성화 + 레이아웃 확인
        if (loadingUI != null)
        {
            loadingUI.SetActive(true);

            // 🔥 위치 재조정: 화면 중앙에 강제로 배치
            RectTransform rt = loadingUI.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = Vector2.zero;
                rt.localScale = Vector3.one;
            }

            // 🔥 알파/캔버스 그룹도 강제 켜기
            CanvasGroup cg = loadingUI.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.alpha = 1f;
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }

            Debug.Log("✅ loadingUI 활성화 완료");
        }
        else
        {
            Debug.LogError("❌ loadingUI == null (참조 없음)");
        }

        yield return null;

        yield return StartCoroutine(Fade(0f, 1f));

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
        {
            if (loadingBar != null)
                loadingBar.value = op.progress;
            yield return null;
        }

        if (loadingBar != null)
            loadingBar.value = 1f;

        yield return new WaitForSeconds(0.5f);
        op.allowSceneActivation = true;
    }

    private IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        Color color = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, t / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, to);
    }
}