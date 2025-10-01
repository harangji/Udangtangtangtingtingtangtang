using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 씬 전환을 관리하는 싱글톤 클래스입니다.
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    /*
    [Header("UI 요소 설정")]
    [Tooltip("로딩 중 표시할 UI 화면 오브젝트입니다.")]
    public GameObject loadingScreen;
    [Tooltip("로딩 진행률을 표시할 이미지입니다. (Image Type: Filled)")]
    public Image progressBar;
    [Tooltip("화면 전환 시 페이드 효과를 위한 검은색 이미지입니다.")]
    public Image fadeImage;
    [Tooltip("페이드 효과에 걸리는 시간입니다.")]
    public float fadeDuration = 0.5f;
    */

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 지정된 이름의 씬을 로드하는 메인 함수입니다.
    public void LoadScene(string sceneName)
    {
        // 간단한 씬 로딩을 위해 비동기 로딩 코루틴을 시작합니다.
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    // 씬 로딩 과정을 처리하는 코루틴입니다.
    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        // // 화면을 검게 만듭니다.
        // yield return StartCoroutine(Fade(0f, 1f));

        // // 로딩 화면을 활성화합니다.
        // if (loadingScreen != null) loadingScreen.SetActive(true);

        // 비동기적으로 씬을 로드합니다.
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);

        // 씬 로딩이 완료될 때까지 대기합니다.
        while (!asyncOp.isDone)
        {
            // // 로딩 바 업데이트 로직
            // float progress = Mathf.Clamp01(asyncOp.progress / 0.9f);
            // if (progressBar != null) progressBar.fillAmount = progress;
            
            yield return null;
        }

        // // 로딩 화면을 비활성화합니다.
        // if (loadingScreen != null) loadingScreen.SetActive(false);

        // // 화면을 다시 밝게 만듭니다.
        // yield return StartCoroutine(Fade(1f, 0f));
    }

    /*
    // 페이드 효과를 처리하는 코루틴입니다.
    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        if (fadeImage == null) yield break;

        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }
    */
}