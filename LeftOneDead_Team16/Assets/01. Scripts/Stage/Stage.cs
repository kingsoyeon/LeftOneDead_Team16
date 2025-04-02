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
        // 두 종류의 적 프리팹 로드
        var zombiePrefab = Resources.Load<GameObject>("Prefabs/Character/Enemy/Zombie");
        var vomitZombiePrefab = Resources.Load<GameObject>("Prefabs/Character/Enemy/VomitZombie");

        for (var i = 0; i < enemyRespawn.Count; i++)
        {
            GameObject go;
            // 0 ~ 1 사이의 랜덤값, 0.7 미만이면 Zombie, 아니면 VomitZombie 소환 (7:3 비율)
            if (UnityEngine.Random.value < 0.7f)
            {
                go = Instantiate(zombiePrefab, enemyRespawn[i].position, enemyRespawn[i].rotation);
            }
            else
            {
                go = Instantiate(vomitZombiePrefab, enemyRespawn[i].position, enemyRespawn[i].rotation);
            }
            enemyList.Add(go.GetComponent<Enemy>());
        }
    }
}