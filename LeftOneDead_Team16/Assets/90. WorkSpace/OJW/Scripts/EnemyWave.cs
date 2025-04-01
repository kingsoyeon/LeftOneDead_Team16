using System;
using System.Collections;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    private int respawnCount;
    private float respawnInterval;
    
    [SerializeField] private GameObject enemyResource;
    [SerializeField] private GameObject specialEnemyResource;

    private void Awake()
    {
        respawnCount = 30;
        respawnInterval = 1f;
    }

    private void Start()
    {
        // test 코드
        StageManager.Instance.AddActionToEventActionDict(0, () => StartCoroutine(Respawn()));
    }

    private void Update()
    {
        // 테스트 코드
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        var curRespawnCount = 0;
        while (curRespawnCount < respawnCount)
        {
            GameObject go;
            if (curRespawnCount % 15 == 14)
            {
                go = Instantiate(specialEnemyResource, transform.position, Quaternion.identity);
            }
            else
            {
                go = Instantiate(enemyResource, transform.position, Quaternion.identity);
            }
            //go.GetComponent<Enemy>().MoveToPosition(StageManager.Instance.Player.transform.position);
            go.GetComponent<Enemy>().startState = EnemyStartState.Trace;
            curRespawnCount++;
            print($"현재 리스폰 된 좀비 수: {curRespawnCount}");
            yield return new WaitForSeconds(respawnInterval);
        }
    }
}
