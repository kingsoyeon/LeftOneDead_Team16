using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 프레임 설정
/// 게임 시작 및 종료, 씬 전환
/// </summary>
public class GameManager : Singleton<GameManager>
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    
    void Update()
    {
        
    }
}
