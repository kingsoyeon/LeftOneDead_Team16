using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private Player player;
    private List<Enemy> enemyList;

    [SerializeField] private Transform playerRespawn;
    [SerializeField] private List<Transform> enemyRespawn;

    public Player Player => player;

    private void Awake()
    {
        enemyList = new();
        RespawnPlayer();
        RespawnInitialEnemy();
        StageManager.Instance.SetCurrentStage(this);
    }

    // 플레이어 생성 및 Player 객체 저장
    private void RespawnPlayer()
    {
        var playerRes = Resources.Load<GameObject>("Prefabs/Character/Player/Player");
        var go = Instantiate(playerRes, playerRespawn.position, playerRespawn.rotation);
        player = go.GetComponent<Player>();
    }

    /// <summary>
    /// 몬스터 초기 배치 기능
    /// </summary>
    private void RespawnInitialEnemy()
    {
        var enemyRes = Resources.Load<GameObject>("Prefabs/Character/Enemy/Zombie");
        for (var i = 0; i < enemyRespawn.Count; i++)
        {
            var go = Instantiate(enemyRes, enemyRespawn[i].position, enemyRespawn[i].rotation);
            enemyList.Add(go.GetComponent<Enemy>());
        }
    }

    public void StartEnemyWave()
    {
        
    }
}