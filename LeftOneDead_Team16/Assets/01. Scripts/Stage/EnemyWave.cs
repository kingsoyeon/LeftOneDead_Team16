using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWave : MonoBehaviour
{
    [SerializeField, Range(0f, 30f)] private float maxRespawnBoundaryRange;
    [SerializeField, Range(0, 30)]private int respawnCount;
    [SerializeField, Range(0f, 40f)]private float respawnInterval;
    
    [SerializeField] private GameObject enemyResource;
    [SerializeField] private GameObject specialEnemyResource;

    private void Start()
    {
        StageManager.Instance.AddActionToEventActionDict(0, () => StartCoroutine(Respawn()));
        StageManager.Instance.AddActionToEventActionDict(1, () => StartCoroutine(Respawn()));
    }

    public IEnumerator Respawn()
    {
        var curRespawnCount = 0;
        while (curRespawnCount < respawnCount)
        {
            GameObject go;
            
            var dir = Random.insideUnitSphere;
            dir.y = 0f;
            var respawnPos = transform.position + dir * Random.Range(-maxRespawnBoundaryRange, maxRespawnBoundaryRange);
            
            if (curRespawnCount % 5 == 4)
            {
                go = Instantiate(specialEnemyResource, respawnPos, Quaternion.identity);
            }
            else
            {
                go = Instantiate(enemyResource, respawnPos, Quaternion.identity);
            }
            go.GetComponent<Enemy>().startState = EnemyStartState.Trace;
            curRespawnCount++;
            yield return new WaitForSeconds(respawnInterval);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRespawnBoundaryRange);
    }
}
