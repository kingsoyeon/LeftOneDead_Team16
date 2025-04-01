using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Respawn : MonoBehaviour
{
    [SerializeField] private GameObject respawnPrefab;
    [SerializeField, Range(0f, 30f)] private float maxRespawnRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RespawnObject();
        }
    }

    private void RespawnObject()
    {
        var dir = Random.insideUnitSphere;
        dir.y = 0f;
        var respawnPos = transform.position + dir * Random.Range(-maxRespawnRange, maxRespawnRange);

        Instantiate(respawnPrefab, respawnPos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRespawnRange);
    }
}