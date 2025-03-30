using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    private StageData data;

    //private Player player;
    private List<Enemy> enemyList;

    public Stage(StageData stageData)
    {
        data = stageData;
        //this.player = player;
    }

    private void RespawnInitialEnemy()
    {
        for (var i = 0; i < data.enemyInitialRespawnPosList.Count; i++)
        {
            var randomIndex = Random.Range(0, data.enemyResourceList.Count);
            var newEnemy = GameObject.Instantiate(data.enemyResourceList[randomIndex]);
        }
    }
}