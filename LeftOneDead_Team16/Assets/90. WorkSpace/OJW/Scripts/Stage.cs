using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    private StageManager stageManager;
    
    private StageData data;

    //private Player player;
    private List<Enemy> enemyList;

    public Stage(StageManager manager, StageData stageData/*, Player player*/)
    {
        stageManager = manager;
        data = stageData;
        //this.player = player;
    }

    /// <summary>
    /// 몬스터 초기 배치 기능
    /// </summary>
    private void RespawnInitialEnemy()
    {
        for (var i = 0; i < data.enemyInitialRespawnPosList.Count; i++)
        {
            var randomIndex = Random.Range(0, data.enemyResourceList.Count);
            var newEnemy = GameObject.Instantiate(data.enemyResourceList[randomIndex]);
            stageManager.EnemyList.Add(newEnemy);
        }
    }
}