using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public enum SceneName
{
    LobbyScene = 0,
    LoadingScene = 1,
    Stage1_1Scene = 2,
    Stage1_2Scene= 3,   
}

/// <summary>
/// 비동기 씬 전환을 담당하는 클래스
/// (로딩씬에만 존재한다)
/// </summary>
public class SceneLoader : MonoBehaviour
{
    public static SceneName sceneName;
    public static StageData CurrentStageData { get; private set; }

    public static GameManager.GameState nextState;
    private LoadingUI loadingUI;
    [SerializeField] private CanvasGroup fadeCanvas;
    public Transform uiTransform;

    private void Start()
    {
        //var prefab = Resources.Load<LoadingUI>("UI/Screen/LoadingUI");
        //loadingUI = Instantiate(prefab, uiTransform);
        //gameObject.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    
    private IEnumerator LoadSceneAsync(SceneName nextScene)
    { 
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)nextScene);
        asyncOperation.allowSceneActivation = false;

        var prefab = Resources.Load<LoadingUI>("UI/Screen/LoadingUI");
        loadingUI = Instantiate(prefab, uiTransform);

        yield return StartCoroutine(Fade(true)); // 페이드 효과 코루틴


        gameObject.SetActive(true);

        float timer = 0f;

        while (!asyncOperation.isDone) 
        {
            yield return null; // 로드가 완료되기 전까지 기다린다.

            timer += Time.deltaTime;

            if(asyncOperation.progress < 0.9f) // 진행도 바
            {
                // 진행도에 따라 바가 차오른다.
                loadingUI.progressBar.fillAmount = Mathf.Lerp(loadingUI.progressBar.fillAmount, asyncOperation.progress, timer);
            }
            else
            {
                 loadingUI.progressBar.fillAmount = Mathf.Lerp(loadingUI.progressBar.fillAmount, 1f, timer);
                if (loadingUI.progressBar.fillAmount >= 0.99f) // 100%까지 차오르면
                {
                    yield return StartCoroutine(Fade(false)); // 페이드아웃
                   
                    asyncOperation.allowSceneActivation = true; // 씬 활성화
                }
            }
            GameManager.Instance.SetGameState(nextState); // 씬 전환 끝난 후 최종 목적지로 상태 업데이트
        }
        
    }
    /// <summary>
    /// 페이드 전환 효과 코루틴
    /// false가 되면 알파값 0
    /// </summary>
    /// <param name="isFadeIn"></param>
    /// <returns></returns>
    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;
        while (timer <= 1f)
        {
            yield return null;
            timer += Time.deltaTime * 2f;
            fadeCanvas.alpha = Mathf.Lerp(isFadeIn ? 0 : 1, isFadeIn ? 1 : 0, timer); // true - 안 투명 -> false - 투명
        }
    }

    public static void SetStageData(StageData data)
    {
        CurrentStageData = data;
        sceneName = (SceneName)Enum.Parse(typeof(SceneName), data.sceneName);
    }
}
