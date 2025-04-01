using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 프레임 설정
/// 게임 시작 및 종료, 씬 전환
/// </summary>
public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 게임 상태
    /// (로비 / 로딩 / 인게임)
    /// </summary>
    public enum GameState 
    { 
        Lobby, 
        Loading, 
        Setting,
        InGame,
    }

    public GameState CurrentState { get; private set; }

    void Start()
    {
        Application.targetFrameRate = 60;
        if(CurrentState == GameState.InGame) Cursor.lockState = CursorLockMode.Locked;

        // ESC를 누르면 CurrentState == Setting으로 전환해야 함. 어디서?
    }

    /// <summary>
    /// 1. 로비에서 스테이지 진입 버튼 2. 2번째 스테이지를 시작할 때 [최종 'StageScene'으로 전환]
    /// </summary>
    public void StageStart()
    {
        CurrentState = GameState.Loading;
        SceneLoader.sceneName = SceneName.Stage1Scene;
        SceneLoader.nextState = GameState.InGame;
        SceneManager.LoadScene("LoadingScene");

    }

    /// <summary>
    /// 1. 2번째 스테이지를 깬 후 2. 게임 중 게임 나가기 버튼 [최종 '로비씬'으로 전환]
    /// </summary>
    public void ChangeToLobby()
    {
        CurrentState = GameState.Loading;
        SceneLoader.sceneName = SceneName.LobbyScene;
        SceneLoader.nextState = GameState.Lobby;
        SceneManager.LoadScene("LoadingScene");

    }

    /// <summary>
    /// 새롭게 전환 될 State를 설정하는 함수
    /// </summary>
    /// <param name="state"></param>
    public void SetGameState(GameState nextState)
    {
        CurrentState = nextState;
    }
}
