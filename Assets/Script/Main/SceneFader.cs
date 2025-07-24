using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;

    private GameObject loadingUI;
    private Image fadeImage;
    private Slider loadingBar;

    public float fadeDuration = 1f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void FadeAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeThenLoad(sceneName));
    }

    private IEnumerator FadeThenLoad(string sceneName)
    {
        // 1. 로딩 UI 준비
        if (loadingUI == null)
        {
            GameObject prefab = Resources.Load<GameObject>("LoadingCanvas");
            if (prefab == null)
            {
                Debug.LogError("Resources/LoadingCanvas 프리팹 없음");
                yield break;
            }

            loadingUI = Instantiate(prefab);
            DontDestroyOnLoad(loadingUI);

            fadeImage = loadingUI.transform.Find("BlackFade").GetComponent<Image>();
            loadingBar = loadingUI.transform.Find("BlackFade/LoadingBar").GetComponent<Slider>();
        }

        loadingUI.SetActive(true);

        // 2. 화면 어두워지기 (페이드 아웃)
        yield return StartCoroutine(Fade(0f, 1f));

        // 3. 씬 비동기 로딩 시작
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        // 4. 슬라이더 값을 실제 로딩 속도에 맞춰 증가
        while (op.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f); // 0~1 보정
            if (loadingBar != null)
                loadingBar.value = Mathf.MoveTowards(loadingBar.value, progress, Time.deltaTime * 0.5f);
            yield return null;
        }

        // 5. 마지막 구간: 슬라이더 1로 마무리
        if (loadingBar != null)
            loadingBar.value = 1f;

        yield return new WaitForSeconds(0.3f); // 살짝 딜레이
        op.allowSceneActivation = true;

        // 6. 씬 넘어간 후 → 다음 프레임 대기
        yield return null;

        // 7. 밝아지기 (페이드 인)
        yield return StartCoroutine(Fade(1f, 0f));

        // 8. 로딩 UI 비활성화
        loadingUI.SetActive(false);
    }

    private IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        Color color = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(from, to, t / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, to);
    }

    public IEnumerator FadeInOnly()
    {
        if (loadingUI == null)
        {
            GameObject prefab = Resources.Load<GameObject>("LoadingCanvas");
            if (prefab == null)
            {
                Debug.LogError("Resources/LoadingCanvas 프리팹 없음");
                yield break;
            }

            loadingUI = Instantiate(prefab);
            DontDestroyOnLoad(loadingUI);

            fadeImage = loadingUI.transform.Find("BlackFade").GetComponent<Image>();
            loadingBar = loadingUI.transform.Find("BlackFade/LoadingBar")?.GetComponent<Slider>();
        }

        loadingUI.SetActive(true);
        if (loadingBar != null) loadingBar.gameObject.SetActive(false);

        yield return StartCoroutine(Fade(1f, 0f)); 

        fadeImage.raycastTarget = false;
        loadingUI.SetActive(false);
    }
}