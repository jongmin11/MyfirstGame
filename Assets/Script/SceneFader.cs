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
                yield break;
            }

            loadingUI = Instantiate(prefab);
            DontDestroyOnLoad(loadingUI);

            fadeImage = loadingUI.transform.Find("BlackFade").GetComponent<Image>();
            loadingBar = loadingUI.transform.Find("BlackFade/LoadingBar").GetComponent<Slider>();
        }

        loadingUI.SetActive(true);
        yield return StartCoroutine(Fade(0f, 1f));  // 어두워지기

        // 2. 씬 비동기 로드
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        // 3. 가짜 로딩 진행률 만들기
        float fakeLoadTime = Random.Range(1.5f, 3.0f);
        float timer = 0f;
        while (timer < fakeLoadTime)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / fakeLoadTime);
            if (loadingBar != null)
                loadingBar.value = progress;
            yield return null;
        }

        // 4. 로딩바 확실히 1까지 도달
        while (loadingBar != null && loadingBar.value < 1f)
        {
            loadingBar.value += Time.deltaTime * 0.5f;
            yield return null;
        }

        // 5. 진짜 씬 로딩 끝났는지 대기
        while (!op.isDone || op.progress < 0.9f)
        {
            yield return null;
        }

        //  여기서만 allowSceneActivation 허용
        yield return new WaitForSeconds(0.5f);
        op.allowSceneActivation = true;

        // 6. 씬 넘어간 후 다음 프레임
        yield return null;

        // 7. 밝아지기
        yield return StartCoroutine(Fade(1f, 0f));

        // 8. 로딩 UI 꺼주기
        loadingUI.SetActive(false);
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